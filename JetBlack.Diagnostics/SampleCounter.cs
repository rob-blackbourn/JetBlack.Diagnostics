using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    /// <summary>
    /// An average counter that shows the average number of operations completed
    /// in one second. When a counter of this type samples the data, each
    /// sampling interrupt returns one or zero. The counter data is the number
    /// of ones that were sampled. It measures time in units of ticks of the
    /// system performance timer.
    /// 
    /// Formula: (N1 – N0) / ((D1 – D0) / F), where the numerator (N) represents
    /// the number of operations completed, the denominator (D) represents elapsed
    /// time in units of ticks of the system performance timer, and F represents
    /// the number of ticks that elapse in one second. F is factored into the
    /// equation so that the result can be displayed in seconds.
    /// </summary>
    public class SampleCounter : ICounter
    {
        /// <summary>
        /// The counter type.
        /// </summary>
        public const PerformanceCounterType CounterType = PerformanceCounterType.SampleCounter;

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
        public SampleCounter(IPerformanceCounterFactory factory, string categoryName, string counterName, bool readOnly)
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
        public SampleCounter(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, bool readOnly)
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
        public SampleCounter(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, string machineName)
            : this(factory.Create(categoryName, counterName, instanceName, machineName))
        {
        }

        private SampleCounter(IPerformanceCounter counter)
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
        /// Increment the counter by one.
        /// </summary>
        /// <returns>The new value of the counter.</returns>
        public long Increment()
        {
            return Counter.Increment();
        }

        /// <summary>
        /// Decrement the counter by one.
        /// </summary>
        /// <returns>The new value of the counter.</returns>
        public long Decrement()
        {
            return Counter.Decrement();
        }

        /// <summary>
        /// Increment the counter by a specific value which can be negative.
        /// </summary>
        /// <param name="value">The value to incrment the counter by.</param>
        /// <returns>The new value of the counter.</returns>
        public long IncrementBy(long value)
        {
            return Counter.IncrementBy(value);
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
