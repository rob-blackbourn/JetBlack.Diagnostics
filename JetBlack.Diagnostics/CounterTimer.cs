using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    /// <summary>
    /// A counter timer.
    /// </summary>
    public class CounterTimer : ICounter
    {
        /// <summary>
        /// The counter type.
        /// </summary>
        public const PerformanceCounterType CounterType = PerformanceCounterType.CounterTimer;

        /// <summary>
        /// The actual performance counter.
        /// </summary>
        public IPerformanceCounter Counter { get; private set; }

        /// <summary>
        /// Construct a single instance counter.
        /// </summary>
        /// <param name="factory">The factory used to create the counter.</param>
        /// <param name="categoryName">The category of the counter.</param>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="readOnly">If true the counter will be read only, otherwise false.</param>
        public CounterTimer(IPerformanceCounterFactory factory, string categoryName, string counterName, bool readOnly)
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
        public CounterTimer(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, bool readOnly)
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
        public LongNumberOfItems(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, string machineName)
            : this(factory.Create(categoryName, counterName, instanceName, machineName))
        {
        }

        private LongNumberOfItems(IPerformanceCounter counter)
        {
            Counter = counter;
        }

        /// <summary>
        /// Put the counter into a valid start state. Not this can only be done to writeable counters.
        /// </summary>
        public void Reset()
        {
            RawValue = 0;
        }

        /// <summary>
        /// The raw value of the counter.
        /// </summary>
        public long RawValue
        {
            get { return Counter.RawValue; }
            set { Counter.RawValue = value; }
        }

        /// <summary>
        /// Increment the counter by a specific value which can be negative.
        /// </summary>
        /// <param name="ticks">The value to incrment the counter by.</param>
        /// <returns>The new value of the counter.</returns>
        public long IncrementBy(long ticks)
        {
            return Counter.IncrementBy(ticks);
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
        /// Returns the information required for a performance counter installer.
        /// </summary>
        /// <param name="counterName">The name of the counter.</param>
        /// <param name="counterHelp">Helpful information about the counter.</param>
        /// <returns>An array of data which can be used to install the counter.</returns>
        public static CounterCreationData[] CreateCounterData(string counterName, string counterHelp)
        {
            return new[]
            {
                new CounterCreationData(counterName, counterHelp, CounterType)
            };
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
