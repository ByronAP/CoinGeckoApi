// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
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
using System.Linq;
using System.Text;
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
        /// Gets or sets whether this instance is using response caching.
        /// </summary>
        /// <value><c>true</c> if this instances cache is enabled; otherwise, <c>false</c>.</value>
        public bool IsCacheEnabled { get { return _cache.Enabled; } set { _cache.Enabled = value; } }

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

            _cache = new MemCache();

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

            _cache = new MemCache();

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

            _cache = new MemCache();

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

            _cache = new MemCache();

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

        internal static async Task<string> GetStringResponseAsync(RestClient client, RestRequest request, MemCache cache, ILogger logger)
        {
            var fullUrl = client.BuildUri(request).ToString();

            try
            {
                if (cache.Enabled && cache.Contains(fullUrl))
                {
                    var cachedResponse = cache.Get(fullUrl);
                    if (cachedResponse != null)
                    {
#if DEBUG
                        Console.WriteLine("CACHE");
#endif
                        return (string)cachedResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "");
            }

            try
            {
                var response = await client.GetAsync(request);
#if DEBUG
                Console.WriteLine("REMOTE");
#endif
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content;

                    if (!string.IsNullOrEmpty(data) && !string.IsNullOrEmpty(data) && cache.Enabled)
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
                            cache?.Set(fullUrl, data, expiry);
                        }
                        else
                        {
                            logger?.LogWarning("The expires header is too far in the future. URL: {FullUrl}", fullUrl);
                        }
                    }

                    return data;
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
                logger?.LogError(ex, "GetStringResponseAsync request failure.");
                throw;
            }
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

        public void ClearCache() => _cache.Clear();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
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

