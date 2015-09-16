using System;

namespace JetBlack.Diagnostics.Monitoring
{
    /// <summary>
    /// An interface for a class which manages a performance counter.
    /// </summary>
    public interface ICounter : IDisposable
    {
        /// <summary>
        /// The performance counter managed by the class instance.
        /// </summary>
        IPerformanceCounter Counter { get; }
    }
}
