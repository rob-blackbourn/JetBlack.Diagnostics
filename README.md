# JetBlack.Diagnostics

## Introduction

This library provides some support code for supplimenting the standard System.Diagnostics assembly.

## Performance counters

A framework for performance counters addresses three issues: ease of development, testability, useability.

### Ease of development & testability

When writing code which contains performance counters, development is hampered by having to register performance
counters before debugging the code. The same problem arises in testing.
This issue is addressed by creating an interface IPerformanceCounter, a factory
IPerformanceCounterFactory. This means that in development and test we can mock the counters
before installing them in production code.

### Useability

Under the hood a performance counter is essentially a long, a timestamp, and a type. The type determines the calculation
performed when the counter is sampled. The system also understands how to combine two counters of specific types
(usually a counter and a base) to provide more sophisticated statics like average time spent performing an
operation. They need to be registered before use; and in the case of composite counters we must ensure the counter and base
counter is registered consequtively and in order.

An example of this is the integer average timeer. It consists of a numerator of type AverageCouter32 and a denominator of
AverageBase. The average time is achieved by incrementing tyhe numerator by the elapsed ticks, and the denominator by one.
When registered we must ensure that the numerator is created before the denominator which must immediately succeed it.

The IntAverageTimer class provides this functionality. The increment method with the supplied elapsed ticks updates the
numerator by the ticks an the denominator by one. A static method can create the counter data for an installer. As the method
uses a factory interface to create it's counters, development and testing are not hampered by the need for registration.

### Example

If we imagine a lazy cache, we might want to monitor the number of time a fetch occurs, and the time it takes to do the fetch.
The monitor claas might look as follows.

```cs
public class CacheMonitor
{
    public const string CountSuffix = "Count";
    public const string AverageFetchSuffix = "AverageFetch";

    public CacheMonitor(IPerformanceCounterFactory factory, string categoryName, string cacheName, bool readOnly)
    {
        Count = new IntNumberOfItems(factory, categoryName, cacheName + "Count", readOnly);
        AverageFetch = new IntAverageTimer(factory, categoryName, cacheName + "AverageFetch", readOnly);

        if (!readOnly)
            Reset();
    }

    public IntNumberOfItems Count { get; private set; }
    public IntAverageTimer AverageFetch { get; private set; }

    public void Reset()
    {
        Count.Reset();
        AverageFetch.Reset();
    }

    public static CounterCreationData[] CreateCounterData(string cacheName)
    {
        return IntNumberOfItems.CreateCounterData(cacheName + CountSuffix, "The number of times the cache has been accessed")
            .Concat(IntAverageTimer.CreateCounterData(cacheName + AverageFetchSuffix, "The average time taken to fetch an item from the cache", "AverageFetch base"))
            .ToArray();
    }
}
```

Note that a counter factory is used to generate the counter to provide mock support.

The cache could be implemented as follows.

```cs
public delegate bool TryGetValue<TKey, TValue>(TKey key, out TValue value);

    public class Cache<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>();
        private readonly TryGetValue<TKey, TValue> _tryGetValue;
        private readonly CacheMonitor _monitor;

        public Cache(TryGetValue<TKey, TValue> tryGetValue, CacheMonitor monitor)
        {
            _tryGetValue = tryGetValue;
            _monitor = monitor;
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_cache.TryGetValue(key, out value))
                return true;

            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                if (!_tryGetValue(key, out value))
                    return false;
                _cache.Add(key, value);
                return true;
            }
            finally
            {
                stopwatch.Stop();
                _monitor.AverageFetch.Increment(stopwatch.ElapsedTicks);
                _monitor.Count.Increment();
            }
        }
    }
```

And an installer.

```cs
public class CacheInstaller : Installer
{
    public CacheInstaller()
    {
        var installer = new PerformanceCounterInstaller
        {
            CategoryName = "Example User Cache",
            CategoryHelp = "An example cache of users.",
            CategoryType = PerformanceCounterCategoryType.SingleInstance,
            UninstallAction = UninstallAction.Remove
        };
        installer.Counters.AddRange(CacheMonitor.CreateCounterData("UserCache"));

        Installers.Add(installer);
    }
}
```
