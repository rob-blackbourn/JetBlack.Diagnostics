using System.Diagnostics;

namespace JetBlack.Diagnostics.Monitoring
{
    /// <summary>
    /// A difference timer that shows the total time in seconds between when the
    /// component  or process started and the time when this value is calculated.
    /// 
    /// Formula: (D0 - N0) / F, where D0 represents the current time, N0
    /// represents the time the object was started, and F represents the number
    /// of time units that elapse in one second. The value of F is factored
    /// into the equation so that the result can be displayed in seconds.
    /// 
    /// Counters of this type include System\ System Up Time.
    /// </summary>
    public class ElapsedTime : ICounter
    {
        private static ICounterCreator _counterCreator;

        /// <summary>
        /// The counter creator.
        /// </summary>
        public static ICounterCreator CounterCreator { get { return _counterCreator ?? (_counterCreator = new CounterCreator(CounterType)); } }

        /// <summary>
        /// The performance counter type.
        /// </summary>
        public const PerformanceCounterType CounterType = PerformanceCounterType.ElapsedTime;

        /// <summary>
        /// The performance counter managed by this class.
        /// </summary>
        public IPerformanceCounter Counter { get; private set; }

        /// <summary>
        /// Construct a single instance counter.
        /// </summary>
        /// <param name="factory">The factory used to create the counter.</param>
        /// <param name="categoryName">The category of the counter.</param>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="readOnly">If true the counter will be read only, otherwise false.</param>
        public ElapsedTime(IPerformanceCounterFactory factory, string categoryName, string counterName, bool readOnly)
            : this(factory.Create(categoryName, counterName, readOnly))
        {
        }

        /// <summary>
        /// Construct a multi instance counter.
        /// </summary>
        /// <param name="factory">The factory used to create the counter.</param>
        /// <param name="categoryName">The category of the counter.</param>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="instanceName">The name of the instance.</param>
        /// <param name="readOnly">If true the counter will be read only, otherwise false.</param>
        public ElapsedTime(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, bool readOnly)
            : this(factory.Create(categoryName, counterName, instanceName, readOnly))
        {
        }

        /// <summary>
        /// Construct a read only performance counter on a remote machine.
        /// </summary>
        /// <param name="factory">The factory used to create the counter.</param>
        /// <param name="categoryName">The category of the counter.</param>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="instanceName">The name of the instance.</param>
        /// <param name="machineName">The machine name.</param>
        public ElapsedTime(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, string machineName)
            : this(factory.Create(categoryName, counterName, instanceName, machineName))
        {
        }

        private ElapsedTime(IPerformanceCounter counter)
        {
            Counter = counter;
        }

        /// <summary>
        /// Put the counter into a valid start state. Not this can only be done to writeable counters.
        /// </summary>
        public void Reset()
        {
            Counter.RawValue = Stopwatch.GetTimestamp();
        }

        /// <summary>
        /// Samples a read only performance counter.
        /// </summary>
        /// <returns>The calculated value.</returns>
        public float NextValue()
        {
            return Counter.NextValue();
        }

        /// <summary>
        /// Release the resources held by the counter.
        /// </summary>
        public void Dispose()
        {
            Counter.Dispose();
        }

        public override string ToString()
        {
            return Counter.ToString();
        }
    }
}
