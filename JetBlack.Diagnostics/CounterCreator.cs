using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    /// <summary>
    /// A class for creating counter data.
    /// </summary>
    class CounterCreator : ICounterCreator
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="counterType">The type of the performance counter.</param>
        public CounterCreator(PerformanceCounterType counterType)
        {
            CounterType = counterType;
        }

        /// <summary>
        /// The type of the performance counter.
        /// </summary>
        public PerformanceCounterType CounterType { get; private set; }

        /// <summary>
        /// Creates the counter data.
        /// </summary>
        /// <param name="counterName">The counter name.</param>
        /// <param name="counterHelp">The help for the counter.</param>
        /// <returns>An array of data which can be used to install the counter.</returns>
        public CounterCreationData[] CreateCounterData(string counterName, string counterHelp)
        {
            return new[]
                {
                    new CounterCreationData(counterName, counterHelp, CounterType)
                };
        }
    }
}
