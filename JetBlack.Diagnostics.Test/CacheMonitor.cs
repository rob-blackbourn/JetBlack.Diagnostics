using System.Diagnostics;
using System.Linq;
using JetBlack.Diagnostics.Monitoring;

namespace JetBlack.Diagnostics.Test
{
    public class CacheMonitor
    {
        public const string CountSuffix = "Count";
        public const string AverageFetchSuffix = "AverageFetch";

        public CacheMonitor(IPerformanceCounterFactory factory, string categoryName, string cacheName, bool readOnly)
        {
            Count = new NumberOfItems32(factory, categoryName, cacheName + "Count", readOnly);
            AverageFetch = new AverageTimer(factory, categoryName, cacheName + "AverageFetch", readOnly);

            if (!readOnly)
                Reset();
        }

        public NumberOfItems32 Count { get; private set; }
        public AverageTimer AverageFetch { get; private set; }

        public void Reset()
        {
            Count.Reset();
            AverageFetch.Reset();
        }

        public static CounterCreationData[] CreateCounterData(string cacheName)
        {
            return NumberOfItems32.CounterCreator.CreateCounterData(cacheName + CountSuffix, "The number of times the cache has been accessed")
                .Concat(AverageTimer.CounterCreator.CreateCounterData(cacheName + AverageFetchSuffix, "The average time taken to fetch an item from the cache"))
                .ToArray();
        }
    }
}
