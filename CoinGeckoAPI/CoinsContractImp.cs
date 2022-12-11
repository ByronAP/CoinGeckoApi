// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="CoinsContractImp.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    /// <summary>
    /// <para>Implementation of the '/coins/contract' API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class CoinsContractImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal CoinsContractImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// Get coin info from contract address.
        /// </summary>
        /// <param name="id">The id of the platform issuing tokens (see <see cref="CoinGeckoClient.GetAssetPlatformsAsync"/> endpoint for list of options).</param>
        /// <param name="contract_address">The token's contract address.</param>
        /// <returns>A Task&lt;<see cref="CoinContractResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: ethereum)</exception>
        /// <exception cref="System.ArgumentNullException">contract_address - Invalid value. Value must be a valid contract address (EX: 0x514910771af9ca656af840dff83e8264ecf986ca)</exception>
        public async Task<CoinContractResponse> GetCoinContractAsync(string id, string contract_address)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid coin id (EX: ethereum)");
            }

            if (string.IsNullOrEmpty(contract_address) || String.IsNullOrWhiteSpace(contract_address))
            {
                throw new ArgumentNullException(nameof(contract_address), "Invalid value. Value must be a valid contract address (EX: 0x514910771af9ca656af840dff83e8264ecf986ca)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", id, "contract", contract_address));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinContractResponse>(jsonStr);
        }

        /// <summary>
        /// Get historical market data include price, market cap, and 24h volume (granularity auto).
        /// </summary>
        /// <param name="id">The id of the platform issuing tokens (see <see cref="CoinGeckoClient.GetAssetPlatformsAsync"/> endpoint for list of options).</param>
        /// <param name="contract_address">The token's contract address.</param>
        /// <param name="vs_currency">The target currency of market data (usd, eur, jpy, etc.).</param>
        /// <param name="days">Data up to number of days ago (eg. 1,14,30,max).</param>
        /// <returns>A Task&lt;<see cref="CoinMarketChartResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: ethereum)</exception>
        /// <exception cref="System.ArgumentNullException">contract_address - Invalid value. Value must be a valid contract address (EX: 0x514910771af9ca656af840dff83e8264ecf986ca)</exception>
        /// <exception cref="System.ArgumentNullException">vs_currency - Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">days - Invalid value. Value must not exceed 900000.</exception>
        public async Task<CoinMarketChartResponse> GetCoinContractMarketChartAsync(string id, string contract_address, string vs_currency, uint days)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid coin id (EX: ethereum)");
            }

            if (string.IsNullOrEmpty(contract_address) || String.IsNullOrWhiteSpace(contract_address))
            {
                throw new ArgumentNullException(nameof(contract_address), "Invalid value. Value must be a valid contract address (EX: 0x514910771af9ca656af840dff83e8264ecf986ca)");
            }

            if (string.IsNullOrEmpty(vs_currency) || String.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(vs_currency), "Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)");
            }

            if (days > 900000)
            {
                throw new ArgumentOutOfRangeException(nameof(days), "Invalid value. Value must not exceed 900000.");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", id, "contract", contract_address, "market_chart"));
            request.AddQueryParameter("vs_currency", vs_currency);
            request.AddQueryParameter("days", days);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinMarketChartResponse>(jsonStr);
        }

        /// <summary>
        /// Get historical market data include price, market cap, and 24h volume within a range of timestamp (granularity auto).
        /// </summary>
        /// <param name="id">The id of the platform issuing tokens (see <see cref="CoinGeckoClient.GetAssetPlatformsAsync"/> endpoint for list of options).</param>
        /// <param name="contract_address">The token's contract address.</param>
        /// <param name="vs_currency">The target currency of market data (usd, eur, jpy, etc.).</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>A Task&lt;<see cref="CoinMarketChartResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: ethereum)</exception>
        /// <exception cref="System.ArgumentNullException">contract_address - Invalid value. Value must be a valid contract address (EX: 0x514910771af9ca656af840dff83e8264ecf986ca)</exception>
        /// <exception cref="System.ArgumentNullException">vs_currency - Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)</exception>
        public async Task<CoinMarketChartResponse> GetCoinContractMarketChartRangeAsync(string id, string contract_address, string vs_currency, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid coin id (EX: ethereum)");
            }

            if (string.IsNullOrEmpty(contract_address) || String.IsNullOrWhiteSpace(contract_address))
            {
                throw new ArgumentNullException(nameof(contract_address), "Invalid value. Value must be a valid contract address (EX: 0x514910771af9ca656af840dff83e8264ecf986ca)");
            }

            if (string.IsNullOrEmpty(vs_currency) || String.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(vs_currency), "Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", id, "contract", contract_address, "market_chart", "range"));
            request.AddQueryParameter("vs_currency", vs_currency);
            request.AddQueryParameter("from", fromDate.ToUnixTimeSeconds());
            request.AddQueryParameter("to", toDate.ToUnixTimeSeconds());

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinMarketChartResponse>(jsonStr);
        }
    }
}
