namespace JetBlack.Diagnostics.Monitoring
{
    /// <summary>
    /// A factory to create performance counters.
    /// </summary>
    public class PerformanceCounterFactory : IPerformanceCounterFactory
    {
        private static PerformanceCounterFactory _instance;

        /// <summary>
        /// Returns a single instance of the factory.
        /// </summary>
        public static PerformanceCounterFactory Singleton { get { return _instance ?? (_instance = new PerformanceCounterFactory()); } }

        /// <summary>
        /// Create a single instance performance counter.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="readOnly">If true the counter will be readonly.</param>
        /// <returns>A performance counter.</returns>
        public IPerformanceCounter Create(string categoryName, string counterName, bool readOnly)
        {
            return new PerformanceCounterImpl(categoryName, counterName, readOnly);
        }

        /// <summary>
        /// Create a multi instance performance counter.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="instanceName">The name of the instance.</param>
        /// <param name="readOnly">If true the counter will be readonly.</param>
        /// <returns>A performance counter.</returns>
        public IPerformanceCounter Create(string categoryName, string counterName, string instanceName, bool readOnly)
        {
            return new PerformanceCounterImpl(categoryName, counterName, instanceName, readOnly);
        }

        /// <summary>
        /// Create a performance counter on a remote machine.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="instanceName">The name of the instance.</param>
        /// <param name="machineName"></param>
        /// <returns>A performance counter.</returns>
        public IPerformanceCounter Create(string categoryName, string counterName, string instanceName, string machineName)
        {
            return new PerformanceCounterImpl(categoryName, counterName, instanceName, machineName);
        }
    }
}
