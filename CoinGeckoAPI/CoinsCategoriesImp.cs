// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="CoinsCategoriesImp.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
using CoinGeckoAPI.Models;
using CoinGeckoAPI.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    /// <summary>
    /// <para>Implementation of the '/coins/categories' API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class CoinsCategoriesImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal CoinsCategoriesImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// List all categories with market data.
        /// </summary>
        /// <param name="order">How the response is ordered (sorting).</param>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="CoinCategoriesItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<CoinCategoriesItem>> GetCoinCategoriesAsync(CoinCategoriesOrderBy order = CoinCategoriesOrderBy.market_cap_desc)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", "categories"));
            if (order != CoinCategoriesOrderBy.market_cap_desc) { request.AddQueryParameter("order", order.ToString()); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinCategoriesItem[]>(jsonStr);
        }

        /// <summary>
        /// List all categories.
        /// </summary>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="CoinCategoriesListItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<CoinCategoriesListItem>> GetCoinCategoriesListAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", "categories", "list"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinCategoriesListItem[]>(jsonStr);
        }
    }
}
