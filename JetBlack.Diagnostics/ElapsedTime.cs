using System;
using System.Diagnostics;

namespace JetBlack.Diagnostics
{
    /// <summary>
    /// A higher level implementation of an elepsed time performance counter.
    /// 
    /// The counter is reset with the current time, and subsequenct calls
    /// to the increment method use a stop watch which was started on reset.
    /// </summary>
    public class ElapsedTime : ICounter
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();

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
            RawValue = DateTime.Now.Ticks;
            _stopWatch.Start();
        }

        /// <summary>
        /// Gets the ticks elapsed.
        /// </summary>
        public long RawValue
        {
            get { return Counter.RawValue; }
            set { Counter.RawValue = value; }
        }

        /// <summary>
        /// Increments the counter with the number of ticks since the last increment or reset.
        /// </summary>
        /// <returns></returns>
        public long Increment()
        {
            var ticks = _stopWatch.ElapsedTicks;
            Counter.RawValue = ticks;
            return ticks;
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
                new CounterCreationData(counterName, counterHelp, PerformanceCounterType.ElapsedTime)
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
