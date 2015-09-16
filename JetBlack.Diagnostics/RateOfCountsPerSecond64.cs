using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    /// <summary>
    /// A difference counter that shows the average number of operations
    /// completed during each second of the sample interval. Counters of this
    /// type measure time in ticks of the system clock. This counter type is
    /// the same as the RateOfCountsPerSecond32 type, but it uses larger fields
    /// to accommodate larger values to track a high-volume number of items or
    /// operations per second, such as a byte-transmission rate.
    /// 
    /// Formula: (N1 - N0) / ((D1 - D0) / F), where N1 and N0 are performance
    /// counter readings, D1 and D0 are their corresponding time readings, and
    /// F represents the number of ticks per second. Thus, the numerator
    /// represents the number of operations performed during the last sample
    /// interval, the denominator represents the number of ticks elapsed during
    /// the last sample interval, and F is the frequency of the ticks. The value
    /// of F is factored into the equation so that the result can be displayed
    /// in seconds.
    /// 
    /// Counters of this type include System\ File Read Bytes/sec.
    /// </summary>
    public class RateOfCountsPerSecond64 : ICounter
    {
        private static ICounterCreator _counterCreator;

        /// <summary>
        /// The counter creator.
        /// </summary>
        public static ICounterCreator CounterCreator { get { return _counterCreator ?? (_counterCreator = new CounterCreator(CounterType)); } }

        /// <summary>
        /// The performance counter type.
        /// </summary>
        public const PerformanceCounterType CounterType = PerformanceCounterType.RateOfCountsPerSecond64;

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
        public RateOfCountsPerSecond64(IPerformanceCounterFactory factory, string categoryName, string counterName, bool readOnly)
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
        public RateOfCountsPerSecond64(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, bool readOnly)
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
        public RateOfCountsPerSecond64(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, string machineName)
            : this(factory.Create(categoryName, counterName, instanceName, machineName))
        {
        }

        private RateOfCountsPerSecond64(IPerformanceCounter counter)
        {
            Counter = counter;
        }

        /// <summary>
        /// The count.
        /// </summary>
        public long RawValue
        {
            get { return Counter.RawValue; }
            set { Counter.RawValue = value; }
        }

        /// <summary>
        /// Put the counter into a valid start state. Not this can only be done to writeable counters.
        /// </summary>
        public void Reset()
        {
            RawValue = 0;
        }

        /// <summary>
        /// Increments the counter by one operation.
        /// </summary>
        /// <returns></returns>
        public long Increment()
        {
            return Counter.Increment();
        }

        /// <summary>
        /// Samples a read only performance counter.
        /// </summary>
        /// <returns></returns>
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
