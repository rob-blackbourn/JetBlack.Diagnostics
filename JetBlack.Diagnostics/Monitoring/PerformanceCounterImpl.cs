using System.Diagnostics;

namespace JetBlack.Diagnostics.Monitoring
{
    /// <summary>
    /// A wrapper class around a true performance counter.
    /// </summary>
    class PerformanceCounterImpl : IPerformanceCounter
    {
        private readonly PerformanceCounter _performanceCounter;

        /// <summary>
        /// Constructs a single instance performance counter.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <param name="counterName">The counter name.</param>
        /// <param name="readOnly">If true the counter is read only, otherwise it is writable.</param>
        public PerformanceCounterImpl(string categoryName, string counterName, bool readOnly)
            : this(new PerformanceCounter(categoryName, counterName, readOnly))
        {
        }

        /// <summary>
        /// Constructs a multi instance read only performance counter.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <param name="counterName">The counter name.</param>
        /// <param name="instanceName">The instance name.</param>
        public PerformanceCounterImpl(string categoryName, string counterName, string instanceName)
            : this(new PerformanceCounter(categoryName, counterName, instanceName))
        {
        }

        /// <summary>
        /// Creates a multi instance performance counter.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <param name="counterName">The counter name.</param>
        /// <param name="instanceName">The instance name.</param>
        /// <param name="readOnly">If true the counter is read only, otherwise it is writable.</param>
        public PerformanceCounterImpl(string categoryName, string counterName, string instanceName, bool readOnly)
            : this(new PerformanceCounter(categoryName, counterName, instanceName, readOnly))
        {
        }

        /// <summary>
        /// Creates a performance counter targetted at a specific machine. Remote counters are always read only.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <param name="counterName">The counter name.</param>
        /// <param name="instanceName">The instance name.</param>
        /// <param name="machineName">The machine name.</param>
        public PerformanceCounterImpl(string categoryName, string counterName, string instanceName, string machineName)
            : this(new PerformanceCounter(categoryName, counterName, instanceName, machineName))
        {
        }

        /// <summary>
        /// Construct a performance counter wrapper from an underlying counter.
        /// </summary>
        /// <param name="performanceCounter"></param>
        public PerformanceCounterImpl(PerformanceCounter performanceCounter)
        {
            _performanceCounter = performanceCounter;
        }

        /// <summary>
        /// The category name.
        /// </summary>
        public string CategoryName
        {
            get { return _performanceCounter.CategoryName; }
            set { _performanceCounter.CategoryName = value; }
        }

        /// <summary>
        /// The counter name.
        /// </summary>
        public string CounterName
        {
            get { return _performanceCounter.CounterName; }
            set { _performanceCounter.CounterName = value; }
        }

        /// <summary>
        /// The instance name.
        /// </summary>
        public string InstanceName
        {
            get { return _performanceCounter.InstanceName; }
            set { _performanceCounter.InstanceName = value; }
        }

        /// <summary>
        /// The machine name.
        /// </summary>
        public string MachineName
        {
            get { return _performanceCounter.MachineName; }
            set { _performanceCounter.MachineName = value; }
        }

        /// <summary>
        /// Provides acces to the raw value of the counter.
        /// </summary>
        public long RawValue
        {
            get { return _performanceCounter.RawValue; }
            set { _performanceCounter.RawValue = value; }
        }

        /// <summary>
        /// If true the counter is read only, otherwise it is writeable.
        /// </summary>
        public bool ReadOnly
        {
            get { return _performanceCounter.ReadOnly; }
            set { _performanceCounter.ReadOnly = value; }
        }

        /// <summary>
        /// Increment the counter.
        /// </summary>
        /// <returns>The incremented value.</returns>
        public long Increment()
        {
            return _performanceCounter.Increment();
        }

        /// <summary>
        /// Decrement the counter.
        /// </summary>
        /// <returns>The decremented value.</returns>
        public long Decrement()
        {
            return _performanceCounter.Decrement();
        }

        /// <summary>
        /// Increment the counter by a specific amount.
        /// </summary>
        /// <param name="value">The amount by which the counter is incremented. This may be negative.</param>
        /// <returns>The new raw value of the counter.</returns>
        public long IncrementBy(long value)
        {
            return _performanceCounter.IncrementBy(value);
        }

        /// <summary>
        /// Takes a sample and returns the calculated value.
        /// </summary>
        /// <returns>The calculated value of the sample taken.</returns>
        public float NextValue()
        {
            return _performanceCounter.NextValue();
        }

        /// <summary>
        /// Closes the performance counter.
        /// </summary>
        public void Dispose()
        {
            _performanceCounter.Close();
        }

        /// <summary>
        /// Provides a string representation of the counter.
        /// </summary>
        /// <returns>A string representation of the counter.</returns>
        public override string ToString()
        {
            return
                string.IsNullOrWhiteSpace(InstanceName)
                ? string.Format("CategoryName=\"{0}\", CounterName=\"{1}\", Value={2}.", CategoryName, CounterName, RawValue)
                : string.Format("CategoryName=\"{0}\", CounterName=\"{1}\", InstanceName=\"{2}\", Value={3}.", CategoryName, CounterName, InstanceName, RawValue);
        }
    }
}
