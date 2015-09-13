using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    /// <summary>
    /// A class which wraps the implementation of an average timer.
    /// </summary>
    public class IntAverageTimer : ICompositeCounter
    {
        /// <summary>
        /// The suffix of the base counter.
        /// </summary>
        public const string BaseSuffix = "Base";

        /// <summary>
        /// The counter type.
        /// </summary>
        public const PerformanceCounterType CounterType = PerformanceCounterType.AverageTimer32;

        /// <summary>
        /// The base counter type.
        /// </summary>
        public const PerformanceCounterType BaseCounterType = PerformanceCounterType.AverageBase;

        /// <summary>
        /// The actual performance counter.
        /// </summary>
        public IPerformanceCounter Counter { get; private set; }

        /// <summary>
        /// The base performance counter.
        /// </summary>
        public IPerformanceCounter CounterBase { get; private set; }

        /// <summary>
        /// Constructs the timer from the two performance counters.
        /// </summary>
        /// <param name="counter">The primary counter.</param>
        /// <param name="counterBase">The base counter.</param>
        private IntAverageTimer(IPerformanceCounter counter, IPerformanceCounter counterBase)
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
        public IntAverageTimer(IPerformanceCounterFactory factory, string categoryName, string counterName, bool readOnly)
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
        public IntAverageTimer(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, bool readOnly)
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
        public IntAverageTimer(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, string machineName)
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
        /// Increments the primary counter by one and the base counter by the elapsed ticks.
        /// </summary>
        /// <param name="elapsedTicks">The number of ticks elapsed while performing a single operation.</param>
        public void Increment(long elapsedTicks)
        {
            Counter.IncrementBy(elapsedTicks);
            CounterBase.Increment();
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
        /// <param name="averageFetchHelp">The help for the primary counter.</param>
        /// <param name="averageFetchBaseHelp">The help for the base counter.</param>
        /// <returns>An array of CounterCreationData that can be used to install the counter.</returns>
        public static CounterCreationData[] CreateCounterData(string counterName, string averageFetchHelp, string averageFetchBaseHelp)
        {
            return new[]
            {
                new CounterCreationData(counterName, averageFetchHelp, PerformanceCounterType.AverageTimer32),
                new CounterCreationData(counterName + BaseSuffix, averageFetchBaseHelp, PerformanceCounterType.AverageBase)
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
