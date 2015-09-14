namespace JetBlack.Diagnostics
{
    /// <summary>
    /// Describes a class which manages composite performance counter.
    /// 
    /// Composite counters are typically used to monitor changes: e.g. average time spent.
    /// </summary>
    public interface ICompositeCounter : ICounter
    {
        /// <summary>
        /// The base performance counter.
        /// </summary>
        IPerformanceCounter CounterBase { get; }
    }
}
