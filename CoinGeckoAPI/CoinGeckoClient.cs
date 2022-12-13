// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-12-2022
// ***********************************************************************
// <copyright file="CoinGeckoClient.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
using CoinGeckoAPI.Exceptions;
using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    /// <summary>
    /// <para>Create an instance of this class to access the API methods.</para>
    /// <para>
    /// Methods and parameters are named as specified in the official
    /// CoinGecko API documentation (Ex: API call '/coins/list' 
    /// translates to 'CoinGeckoClient.Coins.GetCoinsListAsync()').
    /// </para>
    /// <para>By default response caching is enabled. To disable it set <see cref="IsCacheEnabled"/> to <c>false</c>.</para>
    /// <para>By default rate limiting is enabled. To disable it set <see cref="IsRateLimitingEnabled"/> to <c>false</c>.</para>
    /// </summary>
    public class CoinGeckoClient : IDisposable
    {
        /// <summary>
        /// The RestSharp client instance used to make the API calls.
        /// This is exposed in case you wish to change options such as use a proxy.
        /// </summary>
        /// <value>The RestSharp client instance.</value>
        public RestClient CGRestClient { get; }

        /// <summary>
        /// <para>Provides access to the Simple API calls.</para>
        /// An instance of <see cref="SimpleImp"/>.
        /// </summary>
        /// <value>Simple API calls.</value>
        public SimpleImp Simple { get; }

        /// <summary>
        /// <para>Provides access to the Coins API calls.</para>
        /// An instance of <see cref="CoinsImp"/>.
        /// </summary>
        /// <value>Coins API calls.</value>
        public CoinsImp Coins { get; }

        /// <summary>
        /// <para>Provides access to the Exchanges API calls.</para>
        /// An instance of <see cref="ExchangesImp"/>.
        /// </summary>
        /// <value>Exchanges API calls.</value>
        public ExchangesImp Exchanges { get; }

        /// <summary>
        /// <para>Provides access to the Indexes API calls.</para>
        /// An instance of <see cref="IndexesImp"/>.
        /// </summary>
        /// <value>Indexes API calls.</value>
        public IndexesImp Indexes { get; }

        /// <summary>
        /// <para>Provides access to the Derivatives API calls.</para>
        /// An instance of <see cref="DerivativesImp"/>.
        /// </summary>
        /// <value>Derivatives API calls.</value>
        public DerivativesImp Derivatives { get; }

        /// <summary>
        /// <para>Provides access to the Nfts API calls.</para>
        /// An instance of <see cref="NftsImp"/>.
        /// </summary>
        /// <value>Nfts API calls.</value>
        public NftsImp Nfts { get; }

        /// <summary>
        /// <para>Provides access to the Search API calls.</para>
        /// An instance of <see cref="SearchImp"/>.
        /// </summary>
        /// <value>Search API calls.</value>
        public SearchImp Search { get; }

        /// <summary>
        /// <para>Provides access to the Global API calls.</para>
        /// An instance of <see cref="GlobalImp"/>.
        /// </summary>
        /// <value>Global API calls.</value>
        public GlobalImp Global { get; }

        /// <summary>
        /// <para>Provides access to the Companies API calls.</para>
        /// An instance of <see cref="CompaniesImp"/>.
        /// </summary>
        /// <value>Companies API calls.</value>
        public CompaniesImp Companies { get; }

        /// <summary>
        /// <para>Gets or sets whether this instance is using response caching.</para>
        /// <para>Caching is enabled by default.</para>
        /// </summary>
        /// <value><c>true</c> if this instances cache is enabled; otherwise, <c>false</c>.</value>
        public bool IsCacheEnabled { get { return _cache.Enabled; } set { _cache.Enabled = value; } }

        /// <summary>
        /// <para>Gets or sets a value indicating whether rate limiting is enabled.</para>
        /// <para>Rate limiting is enabled by default.</para>
        /// <para>Rate limiting is shared across all instances.</para>
        /// </summary>
        /// <value><c>true</c> if rate limiting is enabled; otherwise, <c>false</c>.</value>
        public static bool IsRateLimitingEnabled { get; set; } = true;

        // this shares the call times across instances ensuring that we don't have an instance making calls
        // with rate limiting off causing an instance that does to have calls fail unexpectedly.
        internal static DateTimeOffset LastApiCallAt { get; set; } = DateTimeOffset.MinValue;
        internal static DateTimeOffset Last429ResponseAt { get; set; } = DateTimeOffset.MinValue;
        internal static int CallsInLast60Seconds { get; set; }
        internal static readonly SemaphoreSlim RateLimitSemaphore = new SemaphoreSlim(1, 1);
        internal static readonly Timer RateLimitTimer = new Timer(RateLimitTimerCallback, null, 60000, 60000);

        private readonly MemCache _cache;
        private bool _disposedValue;
        private readonly ILogger<CoinGeckoClient> _logger;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CoinGeckoClient"/> class.
        /// </summary>
        public CoinGeckoClient()
        {
            _logger = null;

            _cache = new MemCache(_logger);

            CGRestClient = new RestClient(Constants.API_BASE_URL);

            Simple = new SimpleImp(CGRestClient, _cache, _logger);
            Coins = new CoinsImp(CGRestClient, _cache, _logger);
            Exchanges = new ExchangesImp(CGRestClient, _cache, _logger);
            Indexes = new IndexesImp(CGRestClient, _cache, _logger);
            Derivatives = new DerivativesImp(CGRestClient, _cache, _logger);
            Nfts = new NftsImp(CGRestClient, _cache, _logger);
            Search = new SearchImp(CGRestClient, _cache, _logger);
            Global = new GlobalImp(CGRestClient, _cache, _logger);
            Companies = new CompaniesImp(CGRestClient, _cache, _logger);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinGeckoClient"/> class.
        /// </summary>
        /// <param name="isPro">if set to <c>true</c> [is pro].</param>
        public CoinGeckoClient(bool isPro)
        {
            _logger = null;

            _cache = new MemCache(_logger);

            if (isPro)
            {
                CGRestClient = new RestClient(Constants.API_PRO_BASE_URL);
            }
            else
            {
                CGRestClient = new RestClient(Constants.API_BASE_URL);
            }

            Simple = new SimpleImp(CGRestClient, _cache, _logger);
            Coins = new CoinsImp(CGRestClient, _cache, _logger);
            Exchanges = new ExchangesImp(CGRestClient, _cache, _logger);
            Indexes = new IndexesImp(CGRestClient, _cache, _logger);
            Derivatives = new DerivativesImp(CGRestClient, _cache, _logger);
            Nfts = new NftsImp(CGRestClient, _cache, _logger);
            Search = new SearchImp(CGRestClient, _cache, _logger);
            Global = new GlobalImp(CGRestClient, _cache, _logger);
            Companies = new CompaniesImp(CGRestClient, _cache, _logger);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinGeckoClient"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CoinGeckoClient(ILogger<CoinGeckoClient> logger)
        {
            _logger = logger;

            _cache = new MemCache(_logger);

            CGRestClient = new RestClient(Constants.API_BASE_URL);

            Simple = new SimpleImp(CGRestClient, _cache, _logger);
            Coins = new CoinsImp(CGRestClient, _cache, _logger);
            Exchanges = new ExchangesImp(CGRestClient, _cache, _logger);
            Indexes = new IndexesImp(CGRestClient, _cache, _logger);
            Derivatives = new DerivativesImp(CGRestClient, _cache, _logger);
            Nfts = new NftsImp(CGRestClient, _cache, _logger);
            Search = new SearchImp(CGRestClient, _cache, _logger);
            Global = new GlobalImp(CGRestClient, _cache, _logger);
            Companies = new CompaniesImp(CGRestClient, _cache, _logger);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinGeckoClient"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="isPro">if set to <c>true</c> [is pro].</param>
        public CoinGeckoClient(ILogger<CoinGeckoClient> logger, bool isPro)
        {
            _logger = logger;

            _cache = new MemCache(_logger);

            if (isPro)
            {
                CGRestClient = new RestClient(Constants.API_PRO_BASE_URL);
            }
            else
            {
                CGRestClient = new RestClient(Constants.API_BASE_URL);
            }

            Simple = new SimpleImp(CGRestClient, _cache, _logger);
            Coins = new CoinsImp(CGRestClient, _cache, _logger);
            Exchanges = new ExchangesImp(CGRestClient, _cache, _logger);
            Indexes = new IndexesImp(CGRestClient, _cache, _logger);
            Derivatives = new DerivativesImp(CGRestClient, _cache, _logger);
            Nfts = new NftsImp(CGRestClient, _cache, _logger);
            Search = new SearchImp(CGRestClient, _cache, _logger);
            Global = new GlobalImp(CGRestClient, _cache, _logger);
            Companies = new CompaniesImp(CGRestClient, _cache, _logger);
        }
        #endregion

        /// <summary>
        /// Check API server status.
        /// </summary>
        /// <returns>True if the api was successfully reached.</returns>
        public async Task<bool> PingAsync()
        {
            var request = new RestRequest(BuildUrl("ping"));

            try
            {
                var jsonString = await GetStringResponseAsync(CGRestClient, request, _cache, _logger);
                _logger?.LogDebug("{JsonString}", jsonString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get BTC-to-Currency exchange rates.
        /// </summary>
        /// <returns>An instance of <see cref="ExchangeRatesResponse"/></returns>
        public async Task<ExchangeRatesResponse> GetExchangeRatesAsync()
        {
            var request = new RestRequest(BuildUrl("exchange_rates"));

            var jsonString = await GetStringResponseAsync(CGRestClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<ExchangeRatesResponse>(jsonString);
        }

        /// <summary>
        /// List all asset platforms (Blockchain networks).
        /// </summary>
        /// <param name="filter">Apply relevant filters to results. Valid values: "nft" (asset_platform nft-support).</param>
        /// <returns>List all asset_platforms. <see cref="AssetPlatform"/></returns>
        public async Task<IEnumerable<AssetPlatform>> GetAssetPlatformsAsync(string filter = null)
        {
            var request = new RestRequest(BuildUrl("asset_platforms"));

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrWhiteSpace(filter))
            {
                request.AddQueryParameter("filter", filter);
            }

            var jsonString = await GetStringResponseAsync(CGRestClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<IEnumerable<AssetPlatform>>(jsonString);
        }

        /// <summary>
        /// Clears the response cache.
        /// </summary>
        public void ClearCache() => _cache.Clear();

        internal static async Task<string> GetStringResponseAsync(RestClient client, RestRequest request, MemCache cache, ILogger logger)
        {
            var fullUrl = client.BuildUri(request).ToString();

            // we don't cache /ping
            if (!fullUrl.EndsWith("/ping", StringComparison.InvariantCultureIgnoreCase))
            {

                try
                {
                    if (cache.TryGet(fullUrl, out var cacheResponse))
                    {
                        return (string)cacheResponse;
                    }
                }
                catch (Exception ex)
                {
                    logger?.LogError(ex, "");
                }
            }

            try
            {
                await DoRateLimiting();

                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    if (!fullUrl.EndsWith("/ping", StringComparison.InvariantCultureIgnoreCase))
                    {
                        cache.CacheRequest(fullUrl, response);
                    }

                    return response.Content;

                }

                if (response.ErrorException != null)
                {
                    logger?.LogError(response.ErrorException, "GetStringResponseAsync failed.");
                    throw response.ErrorException;
                }

                throw new UnknownException($"Unknown exception, http response code is not success, {response.StatusCode}.");
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLowerInvariant().Contains("toomanyrequests"))
                {
                    Last429ResponseAt = DateTimeOffset.UtcNow;
                    logger?.LogError("API requests rate limited at the server for the next {RateLimitRefreshSeconds} seconds.", Constants.API_RATE_LIMIT_RESET_MS / 1000);
                }
                logger?.LogError(ex, "GetStringResponseAsync request failure.");
                throw;
            }
        }

        internal static async Task DoRateLimiting()
        {
            try
            {
                await RateLimitSemaphore.WaitAsync();

                DateTimeOffset nextCallableTime;
                var currentRPM = CallsInLast60Seconds;

                // this is like a progressive limit
                if (currentRPM >= Constants.API_MAX_RPM / (Constants.API_RATE_LIMIT_MS / 1000))
                {
                    nextCallableTime = DateTimeOffset.UtcNow.AddMilliseconds(Constants.API_RATE_LIMIT_MS);
                }
                else if (currentRPM * 2 >= Constants.API_MAX_RPM / (Constants.API_RATE_LIMIT_MS / 1000))
                {
                    nextCallableTime = LastApiCallAt.AddMilliseconds(Constants.API_RATE_LIMIT_MS / 1.5);
                }
                else
                {
                    nextCallableTime = DateTimeOffset.UtcNow.AddMilliseconds(Constants.API_RATE_LIMIT_MS / 2);
                }

                if (Last429ResponseAt.AddMilliseconds(Constants.API_RATE_LIMIT_RESET_MS) > DateTimeOffset.UtcNow)
                {
                    nextCallableTime = Last429ResponseAt.AddMilliseconds(Constants.API_RATE_LIMIT_RESET_MS);
                }

                if (nextCallableTime > DateTimeOffset.UtcNow)
                {
                    var delayTimeMs = Convert.ToInt32((nextCallableTime - DateTimeOffset.UtcNow).TotalMilliseconds);
                    await Task.Delay(delayTimeMs);
                }

                LastApiCallAt = DateTimeOffset.UtcNow;
                CallsInLast60Seconds++;
            }
            finally { RateLimitSemaphore.Release(); }
        }

        internal static void RateLimitTimerCallback(Object nothing)
        {
            try
            {
                RateLimitSemaphore.Wait();

                CallsInLast60Seconds = 0;
            }
            finally { RateLimitSemaphore.Release(); }
        }

        internal static string BuildUrl(params string[] parts)
        {
            if (parts.Length > 2)
            {
                var sb = new StringBuilder();
                sb.Append("/api/v").Append(Constants.API_VERSION);
                foreach (var part in parts)
                {
                    sb.Append('/');
                    sb.Append(part);
                }
                return sb.ToString();
            }
            else
            {
                var result = $"/api/v{Constants.API_VERSION}";
                foreach (var part in parts)
                {
                    result += $"/{part}";
                }
                return result;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (RateLimitTimer != null)
                    {
                        try
                        {
                            RateLimitTimer.Dispose();
                        }
                        catch
                        {
                            // ignore
                        }
                    }

                    if (RateLimitSemaphore != null)
                    {
                        try
                        {
                            RateLimitSemaphore.Dispose();
                        }
                        catch
                        {
                            // ignore
                        }
                    }

                    if (_cache != null)
                    {
                        try
                        {
                            _cache.Dispose();
                        }
                        catch
                        {
                            // ignore
                        }
                    }
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

