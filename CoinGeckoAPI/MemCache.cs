using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace CoinGeckoAPI
{
    internal class MemCache : IDisposable
    {
        internal bool Enabled { get; set; }

        private readonly ILogger _logger;
        private readonly List<string> _keys;
        private readonly MemoryCache _cache;
        private readonly object _lockObject;

        internal MemCache(ILogger<CoinGeckoClient> logger)
        {
            _logger = logger;
            _cache = new MemoryCache("response-cache");
            _keys = new List<string>();
            _lockObject = new object();
        }

        private void CacheRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            lock (_lockObject)
            {
                _keys.Remove(arguments.CacheItem.Key);
            }
        }

        internal bool Contains(string key) => _cache.Contains(key);

        internal bool TryGet(string key, out object value)
        {
            if (!Enabled)
            {
                _logger?.LogDebug("Cache Disabled for URL: {Key}", key);

                value = null;
                return false;
            }

            if (_cache.Contains(key))
            {
                _logger?.LogDebug("Cache Hit for URL: {Key}", key);

                value = _cache.Get(key);
                return true;
            }
            else
            {
                _logger?.LogDebug("Cache Miss for URL: {Key}", key);
                value = null;
                return false;
            }
        }

        internal void CacheRequest(string key, RestResponse response)
        {
            if (!Enabled) { return; }

            var data = response.Content;

            if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(data))
            {
                var isCFCacheHit = false;
                var ageSeconds = 0;
                var cacheSeconds = 0;

                if (response.Headers.Any(x => x.Name.Equals("CF-Cache-Status", StringComparison.InvariantCultureIgnoreCase)))
                {
                    isCFCacheHit = response.Headers.First(x => x.Name.Equals("CF-Cache-Status", StringComparison.InvariantCultureIgnoreCase)).Value.ToString().Equals("hit", StringComparison.InvariantCultureIgnoreCase);
                }

                if (isCFCacheHit && response.Headers.Any(x => x.Name.Equals("age", StringComparison.InvariantCultureIgnoreCase)))
                {
                    ageSeconds = Convert.ToInt32(response.Headers.First(x => x.Name.Equals("age", StringComparison.InvariantCultureIgnoreCase)).Value);
                }

                if (response.Headers.Any(x => x.Name.Equals("Cache-Control", StringComparison.InvariantCultureIgnoreCase)))
                {
                    var cacheControl = response.Headers.First(x => x.Name.Equals("Cache-Control", StringComparison.InvariantCultureIgnoreCase)).Value.ToString();
                    var parts = cacheControl.Split(',');
                    if (parts.Length > 1)
                    {
                        var cacheControlCacheSeconds = Convert.ToInt32(parts[1].Replace("max-age=", ""));

                        // make sure the data we have is not waiting for CF cache refresh
                        if (cacheControlCacheSeconds > ageSeconds)
                        {
                            cacheSeconds = cacheControlCacheSeconds - ageSeconds;
                        }
                    }
                }

                // keep a minimum cache of 10 seconds
                if (cacheSeconds <= 10) { cacheSeconds = 10; }

                var expiry = DateTimeOffset.UtcNow.AddSeconds(cacheSeconds);

                if (expiry < DateTimeOffset.UtcNow.AddMinutes(4))
                {
                    Set(key, data, expiry);
                    _logger?.LogDebug("Cache Set Expires in: {Expiry} seconds for URL: {Key}", cacheSeconds, key);
                }
                else
                {
                    _logger?.LogWarning("The expires header is too far in the future. URL: {FullUrl}", key);
                }
            }
        }

        private void Set(string key, object value, DateTimeOffset exp)
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
