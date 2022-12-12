using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace CoinGeckoAPI
{
    internal class MemCache : IDisposable
    {
        internal bool Enabled { get; set; }
        private List<string> _keys;
        private MemoryCache _cache;
        private object _lockObject = new object();

        internal MemCache()
        {
            _cache = new MemoryCache("response-cache");
            _keys = new List<string>();
        }

        private void CacheRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            lock (_lockObject)
            {
                _keys.Remove(arguments.CacheItem.Key);
            }
        }

        internal bool Contains(string key) => _cache.Contains(key);

        internal object Get(string key) => _cache.Get(key);

        internal void Set(string key, object value, DateTimeOffset exp)
        {
            lock (_lockObject)
            {
                var cacheItem = new CacheItem(key, value);
                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = exp
                };
                policy.RemovedCallback += CacheRemovedCallback;

                _cache.Set(cacheItem, policy);

                if (!_keys.Contains(key)) { _keys.Add(key); }
            }
        }

        internal void Clear()
        {
            lock (_lockObject)
            {
                var keys = _keys.ToArray();
                foreach (var key in keys)
                {
                    try
                    {
                        _cache.Remove(key);
                    }
                    catch
                    {
                        // ignore
                    }
                }

                try
                {
                    _keys.Clear();
                }
                catch
                {
                    // ignore
                }
            }
        }

        public void Dispose()
        {
            ((IDisposable)_cache).Dispose();
        }
    }
}
