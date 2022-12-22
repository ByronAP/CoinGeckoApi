// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="DerivativesImp.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
using CoinGeckoAPI.Models;
using CoinGeckoAPI.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinGeckoAPI.Imps
{
    /// <summary>
    /// <para>Implementation of the '/derivatives' API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class DerivativesImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;
        private readonly MemCache _cache;

        internal DerivativesImp(RestClient restClient, MemCache cache, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _cache = cache;
            _restClient = restClient;
        }

        /// <summary>
        /// List all derivative tickers.
        /// </summary>
        /// <param name="include_all">Set to <c>true</c> to include all, otherwise only unexpired.</param>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="DerivativesTickerItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<DerivativesTickerItem>> GetDerivativesAsync(bool include_all = false)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("derivatives"));
            if (include_all) { request.AddQueryParameter("include_tickers", "all"); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<DerivativesTickerItem[]>(jsonStr);
        }

        /// <summary>
        /// List all derivative exchanges.
        /// </summary>
        /// <param name="order">The ordering of results (sort) <see cref="DerivativesExchangeOrderBy"/>.</param>
        /// <param name="per_page">Total results per page.</param>
        /// <param name="page">Page through results.</param>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="DerivativesExchangeItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<DerivativesExchangeItem>> GetDerivativesExchangesAsync(DerivativesExchangeOrderBy order = DerivativesExchangeOrderBy.trade_volume_24h_btc_desc, uint per_page = 50, uint page = 1)
        {
            if (page == 0) { page = 1; }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("derivatives", "exchanges"));
            request.AddQueryParameter("order", order.ToString());
            if (per_page != 50) { request.AddQueryParameter("per_page", per_page); }
            if (page != 1) { request.AddQueryParameter("page", page); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<DerivativesExchangeItem[]>(jsonStr);
        }

        /// <summary>
        /// Get derivative exchange data.
        /// </summary>
        /// <param name="id">The derivatives exchange id (can be obtained from <see cref="GetDerivativesExchangesListAsync"/>).</param>
        /// <param name="include_all_tickers">Set to <c>true</c> to include all, otherwise only unexpired.</param>
        /// <returns>A Task&lt;<see cref="DerivativesExchangeDetailItem"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">id - Invalid value. Value must be a valid derivatives exchange (EX: zbg_futures)</exception>
        public async Task<DerivativesExchangeDetailItem> GetDerivativesExchangeAsync(string id, bool include_all_tickers = false)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid derivatives exchange (EX: zbg_futures)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("derivatives", "exchanges", id));
            request.AddQueryParameter("include_tickers", include_all_tickers ? "all" : "unexpired");

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<DerivativesExchangeDetailItem>(jsonStr);
        }

        /// <summary>
        /// List all derivative exchanges name and identifier.
        /// </summary>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="IdNameListItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<IdNameListItem>> GetDerivativesExchangesListAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("derivatives", "exchanges", "list"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<IdNameListItem[]>(jsonStr);
        }
    }
}
