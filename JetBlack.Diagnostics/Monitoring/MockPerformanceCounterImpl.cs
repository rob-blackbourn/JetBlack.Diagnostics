using System.Threading;

namespace JetBlack.Diagnostics.Monitoring
{
    class MockPerformanceCounterImpl : IPerformanceCounter
    {
        private long _rawValue;

        public MockPerformanceCounterImpl(string categoryName, string counterName, bool readOnly)
            : this(categoryName, counterName, null, readOnly)
        {
        }

        public MockPerformanceCounterImpl(string categoryName, string counterName, string instanceName)
        {
            CategoryName = categoryName;
            CounterName = counterName;
            InstanceName = instanceName;
            ReadOnly = true;
        }

        public MockPerformanceCounterImpl(string categoryName, string counterName, string instanceName, bool readOnly)
        {
            CategoryName = categoryName;
            CounterName = counterName;
            InstanceName = instanceName;
            ReadOnly = readOnly;
        }

        public MockPerformanceCounterImpl(string categoryName, string counterName, string instanceName, string machineName)
        {
            CategoryName = categoryName;
            CounterName = counterName;
            InstanceName = instanceName ?? string.Empty;
            MachineName = machineName;
            ReadOnly = true;
        }

        public string CategoryName { get; set; }

        public string CounterName { get; set; }

        public string InstanceName { get; set; }

        public string MachineName { get; set; }

        public long RawValue
        {
            get { return _rawValue; }
            set { Interlocked.Exchange(ref _rawValue, value); }
        }

        public bool ReadOnly { get; set; }

        public long Increment()
        {
            return Interlocked.Increment(ref _rawValue);
        }

        public long Decrement()
        {
            return Interlocked.Decrement(ref _rawValue);
        }

        public long IncrementBy(long value)
        {
            return Interlocked.Add(ref _rawValue, value);
        }

        public float NextValue()
        {
            return _rawValue;
        }

        public void Dispose()
        {
        }

        public override string ToString()
        {
            return
                string.IsNullOrWhiteSpace(InstanceName)
                ? string.Format("CategoryName=\"{0}\", CounterName=\"{1}\", Value={2}.", CategoryName, CounterName, RawValue)
                : string.Format("CategoryName=\"{0}\", CounterName=\"{1}\", InstanceName=\"{2}\", Value={3}.", CategoryName, CounterName, InstanceName, RawValue);
        }
    }
}
