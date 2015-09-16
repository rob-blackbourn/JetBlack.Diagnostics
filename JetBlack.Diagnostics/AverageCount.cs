using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    /// <summary>
    /// An average counter that shows how many items are processed, on average,
    /// during an operation. Counters of this type display a ratio of the items
    /// processed to the number of operations completed. The ratio is
    /// calculated by comparing the number of items processed during the last
    /// interval to the number of operations completed during the last interval.
    /// 
    /// Formula: (N 1 -N 0)/(B 1 -B 0), where N 1 and N 0 are performance
    /// counter readings, and the B 1 and B 0 are their corresponding
    /// AverageBase values. Thus, the numerator represents the numbers of
    /// items processed during the sample interval, and the denominator
    /// represents the number of operations completed during the sample
    /// interval.
    /// 
    /// Counters of this type include PhysicalDisk\ Avg. Disk Bytes/Transfer.
    /// </summary>
    public class AverageCount : ICompositeCounter
    {
        /// <summary>
        /// The suffix of the base counter.
        /// </summary>
        public const string BaseSuffix = "Base";

        /// <summary>
        /// The counter type.
        /// </summary>
        public const PerformanceCounterType CounterType = PerformanceCounterType.AverageCount64;

        /// <summary>
        /// The actual performance counter.
        /// </summary>
        public IPerformanceCounter Counter { get; private set; }

        /// <summary>
        /// The base counter type.
        /// </summary>
        public const PerformanceCounterType BaseCounterType = PerformanceCounterType.AverageBase;

        /// <summary>
        /// The base performance counter.
        /// </summary>
        public IPerformanceCounter CounterBase { get; private set; }

        /// <summary>
        /// Constructs the timer from the two performance counters.
        /// </summary>
        /// <param name="counter">The primary counter.</param>
        /// <param name="counterBase">The base counter.</param>
        private AverageCount(IPerformanceCounter counter, IPerformanceCounter counterBase)
        {
            Counter = counter;
            CounterBase = counterBase;
        }

        /// <summary>
        /// Creates the composite counter. The base suffix is applied to the counter name to create the base performance counter.
        /// </summary>
        /// <param name="factory">The factory for creating the counters.</param>
        /// <param name="categoryName">The category name.</param>
        /// <param name="counterName">The counter name of the primary couter. The base suffice will be used to create the name of the base counter.</param>
        /// <param name="readOnly">If true the counters will be read only, otherwise they will be writeable.</param>
        public AverageCount(IPerformanceCounterFactory factory, string categoryName, string counterName, bool readOnly)
            : this(
                factory.Create(categoryName, counterName, readOnly),
                factory.Create(categoryName, counterName + BaseSuffix, readOnly))
        {
        }

        /// <summary>
        /// Creates the multi instance version of the composite counter. The base suffix is applied to the counter name to create the base performance counter.
        /// </summary>
        /// <param name="factory">The factory for creating the counters.</param>
        /// <param name="categoryName">The category name.</param>
        /// <param name="counterName">The counter name of the primary couter. The base suffice will be used to create the name of the base counter.</param>
        /// <param name="instanceName">The instance name.</param>
        /// <param name="readOnly">If true the counters will be read only, otherwise they will be writeable.</param>
        public AverageCount(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, bool readOnly)
            : this(
                factory.Create(categoryName, counterName, instanceName, readOnly),
                factory.Create(categoryName, counterName + BaseSuffix, instanceName, readOnly))
        {
        }

        /// <summary>
        /// Creates a read only version of the composite counter targeting a specific machine. The base suffix is applied to the counter name to create the base performance counter.
        /// </summary>
        /// <param name="factory">The factory for creating the counters.</param>
        /// <param name="categoryName">The category name.</param>
        /// <param name="counterName">The counter name of the primary couter. The base suffice will be used to create the name of the base counter.</param>
        /// <param name="instanceName">The instance name.</param>
        /// <param name="machineName">The machine name.</param>
        public AverageCount(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, string machineName)
            : this(
                factory.Create(categoryName, counterName, instanceName, machineName),
                factory.Create(categoryName, counterName + BaseSuffix, instanceName, machineName))
        {
        }

        /// <summary>
        /// Resets the counter.
        /// </summary>
        public void Reset()
        {
            Counter.RawValue = 0;
            CounterBase.RawValue = 0;
        }

        /// <summary>
        /// The raw value of the counter.
        /// </summary>
        public int RawValue
        {
            get { return (int)Counter.RawValue; }
            set { Counter.RawValue = value; }
        }

        /// <summary>
        /// The raw value of the base counter.
        /// </summary>
        public long RawValueBase
        {
            get { return CounterBase.RawValue; }
            set { CounterBase.RawValue = value; }
        }

        /// <summary>
        /// Increments the counter by the number of items processed and the number of operations completed.
        /// </summary>
        /// <param name="itemsProcessed">The number of items processed.</param>
        /// <param name="operationsCompleted">The number of operations completed.</param>
        public void Increment(long itemsProcessed, long operationsCompleted)
        {
            Counter.IncrementBy(itemsProcessed);
            CounterBase.IncrementBy(operationsCompleted);
        }

        /// <summary>
        /// Takes a sample and returns the calculated value.
        /// </summary>
        /// <returns>The average time in seconds.</returns>
        public float NextValue()
        {
            return Counter.NextValue();
        }

        /// <summary>
        /// Returns a data structure suitable for installing a counter.
        /// </summary>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="counterHelp">The help for the primary counter.</param>
        /// <param name="counterBaseHelp">The help for the base counter.</param>
        /// <returns>An array of CounterCreationData that can be used to install the counter.</returns>
        public static CounterCreationData[] CreateCounterData(string counterName, string counterHelp, string counterBaseHelp)
        {
            return new[]
            {
                new CounterCreationData(counterName, counterHelp, CounterType),
                new CounterCreationData(counterName + BaseSuffix, counterBaseHelp, BaseCounterType)
            };
        }

        /// <summary>
        /// Release resources used by the counter.
        /// </summary>
        public void Dispose()
        {
            Counter.Dispose();
            CounterBase.Dispose();
        }

        /// <summary>
        /// Creates a string representation of the state of the counter.
        /// </summary>
        /// <returns>The string representation of the counter.</returns>
        public override string ToString()
        {
            return string.Format("Counter={0}, CounterBase={1}", Counter, CounterBase);
        }
    }
}
