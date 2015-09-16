using System.Diagnostics;

namespace JetBlack.Diagnostics.Monitoring
{
    public interface ICounterCreator
    {
        /// <summary>
        /// Creates the counter data.
        /// </summary>
        /// <param name="counterName">The counter name.</param>
        /// <param name="counterHelp">The help for the counter.</param>
        /// <returns>An array of data which can be used to install the counter.</returns>
        CounterCreationData[] CreateCounterData(string counterName, string counterHelp);
    }
}