namespace JetBlack.Diagnostics.Monitoring
{
    /// <summary>
    /// An interface defining the methods required to create performance counters.
    /// </summary>
    public interface IPerformanceCounterFactory
    {
        /// <summary>
        /// Create a single instance performance counter.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <param name="counterName">The counter name.</param>
        /// <param name="readOnly">If true the counter is read only, otherwise it is writable.</param>
        /// <returns>A performance counter.</returns>
        IPerformanceCounter Create(string categoryName, string counterName, bool readOnly);

        /// <summary>
        /// Create an multi instance performance counter.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <param name="counterName">The counter name.</param>
        /// <param name="instanceName">The instance name.</param>
        /// <param name="readOnly">If true the counter is read only, otherwise it is writable.</param>
        /// <returns>A performance counter.</returns>
        IPerformanceCounter Create(string categoryName, string counterName, string instanceName, bool readOnly);

        /// <summary>
        /// Create a performance counter on a remote machine.
        /// 
        /// For a single instance counter pass an empty string to the instance name.
        /// 
        /// You cannot write to a remote performance counter.
        /// </summary>
        /// <param name="categoryName">The category name.</param>
        /// <param name="counterName">The counter name.</param>
        /// <param name="instanceName">The instance name.</param>
        /// <param name="machineName">The machine name.</param>
        /// <returns></returns>
        IPerformanceCounter Create(string categoryName, string counterName, string instanceName, string machineName);
    }
}
