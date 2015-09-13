using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    public class IntNumberOfItems : ICounter
    {
        public const PerformanceCounterType CounterType = PerformanceCounterType.NumberOfItems32;

        public IPerformanceCounter Counter { get; private set; }

        public IntNumberOfItems(IPerformanceCounterFactory factory, string categoryName, string counterName, bool readOnly)
            : this(factory.Create(categoryName, counterName, readOnly))
        {
        }

        public IntNumberOfItems(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, bool readOnly)
            : this(factory.Create(categoryName, counterName, instanceName, readOnly))
        {
        }

        public IntNumberOfItems(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, string machineName)
            : this(factory.Create(categoryName, counterName, instanceName, machineName))
        {
        }

        private IntNumberOfItems(IPerformanceCounter counter)
        {
            Counter = counter;
        }

        public void Reset()
        {
            RawValue = 0;
        }

        public int RawValue
        {
            get { return (int)Counter.RawValue; }
            set { Counter.RawValue = value; }
        }

        public int Increment()
        {
            return (int)Counter.Increment();
        }

        public int Decrement()
        {
            return (int)Counter.Decrement();
        }

        public int IncrementBy(int value)
        {
            return (int)Counter.IncrementBy(value);
        }

        public float NextValue()
        {
            return Counter.NextValue();
        }

        public static CounterCreationData[] CreateCounterData(string counterName, string counterHelp)
        {
            return new[]
            {
                new CounterCreationData(counterName, counterHelp, PerformanceCounterType.NumberOfItems32)
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
