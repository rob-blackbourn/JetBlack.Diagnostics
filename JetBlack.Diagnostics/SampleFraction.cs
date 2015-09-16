using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    /// <summary>
    /// A percentage counter that shows the average ratio of hits to all
    /// operations during the last two sample intervals.
    /// 
    /// Formula: ((N1 - N0) / (D1 - D0)) x 100, where the numerator represents
    /// the number of successful operations during the last sample interval,
    /// and the denominator represents the change in the number of all
    /// operations (of the type measured) completed during the sample interval,
    /// using counters of type SampleBase.
    /// 
    /// Counters of this type include Cache\Pin Read Hits %.
    /// </summary>
    public class SampleFraction : ICompositeCounter
    {
        /// <summary>
        /// The suffix of the base counter.
        /// </summary>
        public const string BaseSuffix = "Base";

        /// <summary>
        /// The counter type.
        /// </summary>
        public const PerformanceCounterType CounterType = PerformanceCounterType.SampleFraction;

        /// <summary>
        /// The actual performance counter.
        /// </summary>
        public IPerformanceCounter Counter { get; private set; }

        /// <summary>
        /// The base counter type.
        /// </summary>
        public const PerformanceCounterType BaseCounterType = PerformanceCounterType.SampleBase;

        /// <summary>
        /// The base performance counter.
        /// </summary>
        public IPerformanceCounter CounterBase { get; private set; }

        /// <summary>
        /// Constructs the timer from the two performance counters.
        /// </summary>
        /// <param name="counter">The primary counter.</param>
        /// <param name="counterBase">The base counter.</param>
        private SampleFraction(IPerformanceCounter counter, IPerformanceCounter counterBase)
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
        public SampleFraction(IPerformanceCounterFactory factory, string categoryName, string counterName, bool readOnly)
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
        public SampleFraction(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, bool readOnly)
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
        public SampleFraction(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, string machineName)
            : this(
                factory.Create(categoryName, counterName, instanceName, machineName),
                factory.Create(categoryName, counterName + BaseSuffix, instanceName, machineName))
        {
        }

        /// <summary>
        /// Resets the counter.
        /// </summary>
        /// <param name="denominator">A fixed denominator.</param>
        public void Reset(long denominator)
        {
            Counter.RawValue = 0;
            CounterBase.RawValue = 0;
        }

        /// <summary>
        /// The raw value of the counter.
        /// </summary>
        public int Numerator
        {
            get { return (int)Counter.RawValue; }
            set { Counter.RawValue = value; }
        }

        /// <summary>
        /// The raw value of the base counter.
        /// </summary>
        public long Denominator
        {
            get { return CounterBase.RawValue; }
            set { CounterBase.RawValue = value; }
        }

        /// <summary>
        /// Increments the numerator.
        /// </summary>
        public void Increment()
        {
            Counter.Increment();
        }

        /// <summary>
        /// Decrements the numerator.
        /// </summary>
        public void Decrement()
        {
            Counter.Increment();
        }

        /// <summary>
        /// Increments the numerator byb a possibly negative value.
        /// </summary>
        /// <param name="value">The value by which the numerator should be incremented.</param>
        public void IncrementBy(long value)
        {
            Counter.IncrementBy(value);
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
