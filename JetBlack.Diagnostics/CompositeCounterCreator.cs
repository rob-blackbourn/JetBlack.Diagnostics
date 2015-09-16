using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    /// <summary>
    /// Creates the counter data for a composite counter.
    /// </summary>
    class CompositeCounterCreator : ICounterCreator
    {
        /// <summary>
        /// The suffix of the base counter.
        /// </summary>
        public const string BaseSuffix = "Base";

        /// <summary>
        /// The constructor for creating composite counter data.
        /// </summary>
        /// <param name="counterType">The counter type.</param>
        /// <param name="baseCounterType">The base counter type.</param>
        public CompositeCounterCreator(PerformanceCounterType counterType, PerformanceCounterType baseCounterType)
        {
            CounterType = counterType;
            BaseCounterType = baseCounterType;
        }

        /// <summary>
        /// The type of the performance counter.
        /// </summary>
        public PerformanceCounterType CounterType { get; private set; }

        /// <summary>
        /// The type of the base performance counter.
        /// </summary>
        public PerformanceCounterType BaseCounterType { get; private set; }

        /// <summary>
        /// Create the counter data for the composite counter.
        /// </summary>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="counterHelp">The help for this counter.</param>
        /// <returns>An array of data which can be used to install the counter.</returns>
        public CounterCreationData[] CreateCounterData(string counterName, string counterHelp)
        {
            return new[]
                {
                    new CounterCreationData(counterName, counterHelp, CounterType),
                    new CounterCreationData(counterName + "Base", "Base: " + counterHelp, BaseCounterType)
                };
        }
    }
}
