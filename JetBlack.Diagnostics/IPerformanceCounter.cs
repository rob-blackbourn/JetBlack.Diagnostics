using System;

namespace JetBlack.Diagnostics
{
    /// <summary>
    /// An interface describing a performance counter.
    /// 
    /// We provide this interface in order to mock counters. If an actual performance counter
    /// is used it must be installed. This is inconvenient for development and testing.
    /// </summary>
    public interface IPerformanceCounter : IDisposable
    {
        /// <summary>
        /// The category name.
        /// </summary>
        string CategoryName { get; set; }

        /// <summary>
        /// The counter name.
        /// </summary>
        string CounterName { get; set; }

        /// <summary>
        /// This instance name of a multi instance counter, otherwise null.
        /// </summary>
        string InstanceName { get; set; }

        /// <summary>
        /// The machine name.
        /// </summary>
        string MachineName { get; set; }

        /// <summary>
        /// If true, the counter is read only, otherwise it may be written to.
        /// </summary>
        bool ReadOnly { get; set; }

        /// <summary>
        /// The raw value of the counter.
        /// </summary>
        long RawValue { get; set; }

        /// <summary>
        /// Increment the counter by 1.
        /// </summary>
        /// <returns></returns>
        long Increment();

        /// <summary>
        /// Decrement the counter by 1.
        /// </summary>
        /// <returns></returns>
        long Decrement();

        /// <summary>
        /// Increment the counter by a signed amount.
        /// </summary>
        /// <param name="value">The (possibly negative) amount by which the counter should be incremented.</param>
        /// <returns>The new raw value of the counter.</returns>
        long IncrementBy(long value);

        /// <summary>
        /// Sample the counter.
        /// </summary>
        /// <returns>The calculated value of the next sample.</returns>
        float NextValue();
    }
}
