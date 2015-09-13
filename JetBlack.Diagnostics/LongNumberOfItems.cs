using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    public class LongNumberOfItems : ICounter
    {
        public const PerformanceCounterType CounterType = PerformanceCounterType.NumberOfItems64;

        public IPerformanceCounter Counter { get; private set; }

        public LongNumberOfItems(IPerformanceCounterFactory factory, string categoryName, string counterName, bool readOnly)
            : this(factory.Create(categoryName, counterName, readOnly))
        {
        }

        public LongNumberOfItems(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, bool readOnly)
            : this(factory.Create(categoryName, counterName, instanceName, readOnly))
        {
        }

        public LongNumberOfItems(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, string machineName)
            : this(factory.Create(categoryName, counterName, instanceName, machineName))
        {
        }

        private LongNumberOfItems(IPerformanceCounter counter)
        {
            Counter = counter;
        }

        public void Reset()
        {
            RawValue = 0;
        }

        public long RawValue
        {
            get { return Counter.RawValue; }
            set { Counter.RawValue = value; }
        }

        public long Increment()
        {
            return Counter.Increment();
        }

        public long Decrement()
        {
            return Counter.Decrement();
        }

        public long IncrementBy(long value)
        {
            return Counter.IncrementBy(value);
        }

        public float NextValue()
        {
            return Counter.NextValue();
        }

        public static CounterCreationData[] CreateCounterData(string counterName, string counterHelp)
        {
            return new[]
            {
                new CounterCreationData(counterName, counterHelp, PerformanceCounterType.NumberOfItems64)
            };
        }

        public void Dispose()
        {
            Counter.Dispose();
        }

        public override string ToString()
        {
            return Counter.ToString();
        }
    }
}
