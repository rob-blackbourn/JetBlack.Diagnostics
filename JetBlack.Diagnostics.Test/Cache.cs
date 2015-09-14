using System.Collections.Generic;
using System.Diagnostics;

namespace JetBlack.Diagnostics.Test
{
    public delegate bool TryGetValue<TKey, TValue>(TKey key, out TValue value);

    public class Cache<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>();
        private readonly TryGetValue<TKey, TValue> _tryGetValue;
        private readonly CacheMonitor _monitor;

        public Cache(TryGetValue<TKey, TValue> tryGetValue, CacheMonitor monitor)
        {
            _tryGetValue = tryGetValue;
            _monitor = monitor;
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_cache.TryGetValue(key, out value))
                return true;

            var stopwatch = new Stopwatch();
            try
            {
                stopwatch.Start();
                if (!_tryGetValue(key, out value))
                    return false;
                _cache.Add(key, value);
                return true;
            }
            finally
            {
                stopwatch.Stop();
                _monitor.AverageFetch.Increment(stopwatch.ElapsedTicks);
                _monitor.Count.Increment();
            }
        }
    }
}
