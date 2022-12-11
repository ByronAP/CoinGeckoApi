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
    public class CoinGeckoClient
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

        private readonly ILogger<CoinGeckoClient> _logger;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CoinGeckoClient"/> class.
        /// </summary>
        public CoinGeckoClient()
        {
            _logger = null;

            CGRestClient = new RestClient(Constants.API_BASE_URL);

            Simple = new SimpleImp(CGRestClient, _logger);
            Coins = new CoinsImp(CGRestClient, _logger);
            Exchanges = new ExchangesImp(CGRestClient, _logger);
            Indexes = new IndexesImp(CGRestClient, _logger);
            Derivatives = new DerivativesImp(CGRestClient, _logger);
            Nfts = new NftsImp(CGRestClient, _logger);
            Search = new SearchImp(CGRestClient, _logger);
            Global = new GlobalImp(CGRestClient, _logger);
            Companies = new CompaniesImp(CGRestClient, _logger);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinGeckoClient"/> class.
        /// </summary>
        /// <param name="isPro">if set to <c>true</c> [is pro].</param>
        public CoinGeckoClient(bool isPro)
        {
            _logger = null;

            if (isPro)
            {
                CGRestClient = new RestClient(Constants.API_PRO_BASE_URL);
            }
            else
            {
                CGRestClient = new RestClient(Constants.API_BASE_URL);
            }

            Simple = new SimpleImp(CGRestClient, _logger);
            Coins = new CoinsImp(CGRestClient, _logger);
            Exchanges = new ExchangesImp(CGRestClient, _logger);
            Indexes = new IndexesImp(CGRestClient, _logger);
            Derivatives = new DerivativesImp(CGRestClient, _logger);
            Nfts = new NftsImp(CGRestClient, _logger);
            Search = new SearchImp(CGRestClient, _logger);
            Global = new GlobalImp(CGRestClient, _logger);
            Companies = new CompaniesImp(CGRestClient, _logger);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinGeckoClient"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public CoinGeckoClient(ILogger<CoinGeckoClient> logger)
        {
            _logger = logger;

            CGRestClient = new RestClient(Constants.API_BASE_URL);

            Simple = new SimpleImp(CGRestClient, _logger);
            Coins = new CoinsImp(CGRestClient, _logger);
            Exchanges = new ExchangesImp(CGRestClient, _logger);
            Indexes = new IndexesImp(CGRestClient, _logger);
            Derivatives = new DerivativesImp(CGRestClient, _logger);
            Nfts = new NftsImp(CGRestClient, _logger);
            Search = new SearchImp(CGRestClient, _logger);
            Global = new GlobalImp(CGRestClient, _logger);
            Companies = new CompaniesImp(CGRestClient, _logger);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoinGeckoClient"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="isPro">if set to <c>true</c> [is pro].</param>
        public CoinGeckoClient(ILogger<CoinGeckoClient> logger, bool isPro)
        {
            _logger = logger;

            if (isPro)
            {
                CGRestClient = new RestClient(Constants.API_PRO_BASE_URL);
            }
            else
            {
                CGRestClient = new RestClient(Constants.API_BASE_URL);
            }

            Simple = new SimpleImp(CGRestClient, _logger);
            Coins = new CoinsImp(CGRestClient, _logger);
            Exchanges = new ExchangesImp(CGRestClient, _logger);
            Indexes = new IndexesImp(CGRestClient, _logger);
            Derivatives = new DerivativesImp(CGRestClient, _logger);
            Nfts = new NftsImp(CGRestClient, _logger);
            Search = new SearchImp(CGRestClient, _logger);
            Global = new GlobalImp(CGRestClient, _logger);
            Companies = new CompaniesImp(CGRestClient, _logger);
        }
        #endregion

        internal static async Task<string> GetStringResponseAsync(RestClient client, RestRequest request, ILogger logger)
        {
            try
            {
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
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
                var jsonString = await GetStringResponseAsync(CGRestClient, request, _logger);
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

            var jsonString = await GetStringResponseAsync(CGRestClient, request, _logger);

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

            var jsonString = await GetStringResponseAsync(CGRestClient, request, _logger);

            return JsonConvert.DeserializeObject<IEnumerable<AssetPlatform>>(jsonString);
        }
    }
}
