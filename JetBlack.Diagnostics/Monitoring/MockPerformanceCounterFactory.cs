namespace JetBlack.Diagnostics.Monitoring
{
    /// <summary>
    /// Creates a performance counter factory suitable for development and testing.
    /// </summary>
    public class MockPerformanceCounterFactory : IPerformanceCounterFactory
    {
        private static MockPerformanceCounterFactory _instance;

        /// <summary>
        /// Return s single instance.
        /// </summary>
        public static MockPerformanceCounterFactory Singleton { get { return _instance ?? (_instance = new MockPerformanceCounterFactory()); } }

        /// <summary>
        /// Create a single instance performance counter.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="readOnly">If true the counter will be readonly.</param>
        /// <returns>A mock performance counter.</returns>
        public IPerformanceCounter Create(string categoryName, string counterName, bool readOnly)
        {
            return new MockPerformanceCounterImpl(categoryName, counterName, readOnly);
        }

        /// <summary>
        /// Create a multi instance performance counter.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="instanceName">The name of the instance.</param>
        /// <param name="readOnly">If true the counter will be readonly.</param>
        /// <returns>A mock performance counter.</returns>
        public IPerformanceCounter Create(string categoryName, string counterName, string instanceName, bool readOnly)
        {
            return new MockPerformanceCounterImpl(categoryName, counterName, instanceName, readOnly);
        }

        /// <summary>
        /// Create a remote performance counter.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="instanceName">The name of the instance.</param>
        /// <param name="machineName">The machine name.</param>
        /// <returns></returns>
        public IPerformanceCounter Create(string categoryName, string counterName, string instanceName, string machineName)
        {
            return new MockPerformanceCounterImpl(categoryName, counterName, instanceName, machineName);
        }
    }
}
