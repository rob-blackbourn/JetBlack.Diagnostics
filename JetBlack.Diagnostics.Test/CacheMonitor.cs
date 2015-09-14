using System.Diagnostics;
using System.Linq;

namespace JetBlack.Diagnostics.Test
{
    public class CacheMonitor
    {
        public const string CountSuffix = "Count";
        public const string AverageFetchSuffix = "AverageFetch";

        public CacheMonitor(IPerformanceCounterFactory factory, string categoryName, string cacheName, bool readOnly)
        {
            Count = new IntNumberOfItems(factory, categoryName, cacheName + "Count", readOnly);
            AverageFetch = new IntAverageTimer(factory, categoryName, cacheName + "AverageFetch", readOnly);

            if (!readOnly)
                Reset();
        }

        public IntNumberOfItems Count { get; private set; }
        public IntAverageTimer AverageFetch { get; private set; }

        public void Reset()
        {
            Count.Reset();
            AverageFetch.Reset();
        }

        public static CounterCreationData[] CreateCounterData(string cacheName)
        {
            return IntNumberOfItems.CreateCounterData(cacheName + CountSuffix, "The number of times the cache has been accessed")
                .Concat(IntAverageTimer.CreateCounterData(cacheName + AverageFetchSuffix, "The average time taken to fetch an item from the cache", "AverageFetch base"))
                .ToArray();
        }
    }
}
