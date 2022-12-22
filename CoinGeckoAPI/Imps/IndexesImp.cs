// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="IndexesImp.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinGeckoAPI.Imps
{
    /// <summary>
    /// <para>Implementation of the '/indexes' API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class IndexesImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;
        private readonly MemCache _cache;

        internal IndexesImp(RestClient restClient, MemCache cache, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _cache = cache;
            _restClient = restClient;
        }

        /// <summary>
        /// List all market indexes.
        /// </summary>
        /// <param name="per_page">Total results per page.</param>
        /// <param name="page">Page through results.</param>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="IndexItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<IndexItem>> GetIndexesAsync(uint per_page = 50, uint page = 1)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("indexes"));
            if (per_page != 50) { request.AddQueryParameter("per_page", per_page); }
            if (page != 0 && page != 1) { request.AddQueryParameter("page", page); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<IndexItem[]>(jsonStr);
        }

        /// <summary>
        /// Get market index by market id and index id.
        /// </summary>
        /// <param name="market_id">The market id (can be obtained from <see cref="ExchangesImp.GetExchangesListAsync"/>).</param>
        /// <param name="id">The index id (can be obtained from <see cref="GetIndexesListAsync"/>).</param>
        /// <returns>A Task&lt;<see cref="IndexItem"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">market_id - Invalid value. Value must be a valid market id (EX: cme_futures)</exception>
        /// <exception cref="ArgumentNullException">id - Invalid value. Value must be a valid index id (EX: btc)</exception>
        public async Task<IndexItem> GetIndexAsync(string market_id, string id)
        {
            if (string.IsNullOrEmpty(market_id) || string.IsNullOrWhiteSpace(market_id))
            {
                throw new ArgumentNullException(nameof(market_id), "Invalid value. Value must be a valid market id (EX: cme_futures)");
            }
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid index id (EX: btc)");
            }
            var request = new RestRequest(CoinGeckoClient.BuildUrl("indexes", market_id, id));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            var result = JsonConvert.DeserializeObject<IndexItem>(jsonStr);

            result.Id = id;

            return result;
        }

        /// <summary>
        /// List market indexes id and name.
        /// </summary>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="IndexListItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<IndexListItem>> GetIndexesListAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("indexes", "list"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<IndexListItem[]>(jsonStr);
        }
    }
}
