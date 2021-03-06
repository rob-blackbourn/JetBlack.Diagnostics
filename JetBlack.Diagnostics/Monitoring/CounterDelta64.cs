﻿using System.Diagnostics;

namespace JetBlack.Diagnostics.Monitoring
{
    /// <summary>
    /// A difference counter that shows the change in the measured attribute
    /// between the two most recent sample intervals. It is the same as the
    /// CounterDelta32 counter type except that is uses larger fields to
    /// accomodate larger values.
    /// 
    /// Formula: N1 - N0, where N1 and N0 are performance counter readings.
    /// </summary>
    public class CounterDelta64 : ICounter
    {
        private static ICounterCreator _counterCreator;

        /// <summary>
        /// The counter creator.
        /// </summary>
        public static ICounterCreator CounterCreator { get { return _counterCreator ?? (_counterCreator = new CounterCreator(CounterType)); } }

        /// <summary>
        /// The counter type.
        /// </summary>
        public const PerformanceCounterType CounterType = PerformanceCounterType.CounterDelta64;

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
        public CounterDelta64(IPerformanceCounterFactory factory, string categoryName, string counterName, bool readOnly)
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
        public CounterDelta64(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, bool readOnly)
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
        public CounterDelta64(IPerformanceCounterFactory factory, string categoryName, string counterName, string instanceName, string machineName)
            : this(factory.Create(categoryName, counterName, instanceName, machineName))
        {
        }

        private CounterDelta64(IPerformanceCounter counter)
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
