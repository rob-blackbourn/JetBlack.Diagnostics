using System.Diagnostics;

namespace JetBlack.Diagnostics.Monitoring
{
    public interface ICompositeCounterCreator : ICounterCreator
    {
        /// <summary>
        /// Creates the counter data for a composite counter.
        /// </summary>
        /// <param name="counterName">The counter name.</param>
        /// <param name="counterHelp">The help for the counter.</param>
        /// <param name="baseCounterName">The base counter name.</param>
        /// <param name="baseCounterHelp">The base counter help.</param>
        /// <returns>An array of data which can be used to install the counter.</returns>
        CounterCreationData[] CreateCounterData(string counterName, string counterHelp, string baseCounterName, string baseCounterHelp);
    }
}