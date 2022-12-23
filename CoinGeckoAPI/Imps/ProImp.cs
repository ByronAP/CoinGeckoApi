// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-22-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-22-2022
// ***********************************************************************
// <copyright file="ProImp.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
using CoinGeckoAPI.Models;
using CoinGeckoAPI.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace CoinGeckoAPI.Imps
{
    /// <summary>
    /// <para>Implementation of the PRO API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class ProImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;
        private readonly MemCache _cache;

        internal ProImp(RestClient restClient, MemCache cache, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _cache = cache;
            _restClient = restClient;
        }

        /// <summary>
        /// Get historical global market cap and volume data, by number of days away from now as an asynchronous operation.
        /// </summary>
        /// <param name="days">The number of days ago.</param>
        /// <param name="vs_currency">The vs currency (ex: usd).<see cref="SimpleImp.GetSupportedVSCurrenciesAsync"/></param>
        /// <seealso href="https://coingeckoapi.notion.site/coingeckoapi/CoinGecko-Pro-API-exclusive-endpoints-529f4bb5c4d84d5fad797b09cfdb4b53#4bc9bc1d3cfa46288ea93b3e6beb4776"/>
        /// <returns>A Task&lt;<see cref="GlobalMarketCapChartResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.NotSupportedException">This call is restricted to PRO accounts with an API key.</exception>
        /// <exception cref="System.ArgumentNullException">vs_currency - Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)</exception>
        public async Task<GlobalMarketCapChartResponse> GetGlobalMarketCapChartAsync(uint days, string vs_currency = "usd")
        {
            if (_restClient.Options.BaseUrl.ToString() != Constants.API_PRO_BASE_URL)
            {
                throw new NotSupportedException("This call is restricted to PRO accounts with an API key.");
            }

            if (days <= 0)
            {
                // since other endpoints accept 0 as 1 we will just make this act in the same way
                days = 1;
            }

            if (string.IsNullOrEmpty(vs_currency) || string.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(vs_currency), "Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("global", "market_cap_chart"));
            request.AddQueryParameter("vs_currency", vs_currency);
            request.AddQueryParameter("days", days);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<GlobalMarketCapChartResponse>(jsonStr);
        }

        /// <summary>
        /// Get all supported NFT floor price, market cap, volume and market related data on CoinGecko as an asynchronous operation.
        /// </summary>
        /// <param name="asset_platform_id">The NFT platform identifier (ex: ethereum).<see cref="CoinGeckoClient.GetAssetPlatformsAsync"/></param>
        /// <param name="order">The order (sort).</param>
        /// <param name="per_page">The total results per page.</param>
        /// <param name="page">The page through results.</param>
        /// <seealso href="https://coingeckoapi.notion.site/coingeckoapi/CoinGecko-Pro-API-exclusive-endpoints-529f4bb5c4d84d5fad797b09cfdb4b53#fea7b42bfa3647b89ab1e6b44b86b1c4"/>
        /// <returns>A Task&lt;<see cref="NftsMarketsResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.NotSupportedException">This call is restricted to PRO accounts with an API key.</exception>
        public async Task<NftsMarketsResponse> GetNftsMarketsAsync(string asset_platform_id, NftsMarketsOrderBy order = NftsMarketsOrderBy.market_cap_usd_desc, uint per_page = 100, uint page = 1)
        {
            if (_restClient.Options.BaseUrl.ToString() != Constants.API_PRO_BASE_URL)
            {
                throw new NotSupportedException("This call is restricted to PRO accounts with an API key.");
            }

            if (per_page > 250) { per_page = 250; }
            if (page == 0) { page = 1; }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("nfts", "markets"));
            request.AddQueryParameter("asset_platform_id", asset_platform_id);
            request.AddQueryParameter("order", order.ToString().ToLowerInvariant());
            request.AddQueryParameter("per_page", per_page);
            request.AddQueryParameter("page", page);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<NftsMarketsResponse>(jsonStr);
        }

        /// <summary>
        /// Get historical market data of a NFT collection, including floor price, market cap, and 24h volume, by number of days away from now as an asynchronous operation.
        /// </summary>
        /// <param name="id">The id of NFT collection (ex: bored-ape-yacht-club).<see cref="NftsImp.GetNftsListAsync"/></param>
        /// <param name="days">The data up to number of days ago.</param>
        /// <seealso href="https://coingeckoapi.notion.site/coingeckoapi/CoinGecko-Pro-API-exclusive-endpoints-529f4bb5c4d84d5fad797b09cfdb4b53#5f11462529ae4a83ac0945a3ef01c4b1"/>
        /// <returns>A Task&lt;<see cref="NftsMarketChartResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.NotSupportedException">This call is restricted to PRO accounts with an API key.</exception>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid NFT collection id (ex: bored-ape-yacht-club)</exception>
        public async Task<NftsMarketChartResponse> GetNftsMarketChartAsync(string id, uint days)
        {
            if (_restClient.Options.BaseUrl.ToString() != Constants.API_PRO_BASE_URL)
            {
                throw new NotSupportedException("This call is restricted to PRO accounts with an API key.");
            }

            if (days <= 0)
            {
                // since other endpoints accept 0 as 1 we will just make this act in the same way
                days = 1;
            }

            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid NFT collection id (ex: bored-ape-yacht-club)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("nfts", id, "market_chart"));
            request.AddQueryParameter("days", days);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<NftsMarketChartResponse>(jsonStr);
        }

        /// <summary>
        /// Get historical market data of a NFT collection using contract address, including floor price, market cap, and 24h volume, by number of days away from now as an asynchronous operation.
        /// </summary>
        /// <param name="asset_platform_id">The NFT platform identifier (ex: ethereum).<see cref="CoinGeckoClient.GetAssetPlatformsAsync"/></param>
        /// <param name="contract_address">The contract address (ex: 0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d).</param>
        /// <seealso href="https://coingeckoapi.notion.site/coingeckoapi/CoinGecko-Pro-API-exclusive-endpoints-529f4bb5c4d84d5fad797b09cfdb4b53#c18e814fd4664e0b9ac68b9d69b03400"/>
        /// <param name="days">The data up to number of days ago.</param>
        /// <returns>A Task&lt;<see cref="NftsMarketChartResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.NotSupportedException">This call is restricted to PRO accounts with an API key.</exception>
        /// <exception cref="System.ArgumentNullException">asset_platform_id - Invalid value. Value must be a valid NFT collection platform id (ex: ethereum)</exception>
        /// <exception cref="System.ArgumentNullException">contract_address - Invalid value. Value must be a valid NFT collection contract address (ex: 0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d)</exception>
        public async Task<NftsMarketChartResponse> GetNftsMarketChartAsync(string asset_platform_id, string contract_address, uint days)
        {
            if (_restClient.Options.BaseUrl.ToString() != Constants.API_PRO_BASE_URL)
            {
                throw new NotSupportedException("This call is restricted to PRO accounts with an API key.");
            }

            if (days <= 0)
            {
                // since other endpoints accept 0 as 1 we will just make this act in the same way
                days = 1;
            }

            if (string.IsNullOrEmpty(asset_platform_id) || string.IsNullOrWhiteSpace(asset_platform_id))
            {
                throw new ArgumentNullException(nameof(asset_platform_id), "Invalid value. Value must be a valid NFT collection platform id (ex: ethereum)");
            }

            if (string.IsNullOrEmpty(contract_address) || string.IsNullOrWhiteSpace(contract_address))
            {
                throw new ArgumentNullException(nameof(contract_address), "Invalid value. Value must be a valid NFT collection contract address (ex: 0xbc4ca0eda7647a8ab7c2061c2e118a18a936f13d)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("nfts", asset_platform_id, "contract", contract_address, "market_chart"));
            request.AddQueryParameter("days", days);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<NftsMarketChartResponse>(jsonStr);
        }

        /// <summary>
        /// Get the latest floor price and 24h volume of a NFT collection, on each NFT marketplace, e.g. OpenSea and Looksrare as an asynchronous operation.
        /// </summary>
        /// <param name="id">The id of NFT collection (ex: bored-ape-yacht-club).<see cref="NftsImp.GetNftsListAsync"/></param>
        /// <seealso href="https://coingeckoapi.notion.site/coingeckoapi/CoinGecko-Pro-API-exclusive-endpoints-529f4bb5c4d84d5fad797b09cfdb4b53#60b48dc3f220434a91b6cc6f5b8fcae9"/>
        /// <returns>A Task&lt;<see cref="NftsTickersResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.NotSupportedException">This call is restricted to PRO accounts with an API key.</exception>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid NFT collection id (ex: bored-ape-yacht-club)</exception>
        public async Task<NftsTickersResponse> GetNftsTickersAsync(string id)
        {
            if (_restClient.Options.BaseUrl.ToString() != Constants.API_PRO_BASE_URL)
            {
                throw new NotSupportedException("This call is restricted to PRO accounts with an API key.");
            }

            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid NFT collection id (ex: bored-ape-yacht-club)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("nfts", id, "tickers"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<NftsTickersResponse>(jsonStr);
        }
    }
}
