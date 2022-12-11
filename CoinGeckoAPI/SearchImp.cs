// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="SearchImp.cs" company="ByronAP">
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
    /// <para>Implementation of the '/search' API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class SearchImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal SearchImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// Search for coins, categories and markets listed on CoinGecko ordered by largest Market Cap first.
        /// </summary>
        /// <param name="query">The search query string.</param>
        /// <returns>A Task&lt;<see cref="SearchResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">query - Invalid value. Value must be a valid search term.</exception>
        public async Task<SearchResponse> GetSearchAsync(string query)
        {
            if (string.IsNullOrEmpty(query) || String.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query), "Invalid value. Value must be a valid search term.");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("search"));
            request.AddQueryParameter("query", query);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<SearchResponse>(jsonStr);
        }

        /// <summary>
        /// Get trending search coins (Top-7) on CoinGecko in the last 24 hours.
        /// <para>Top-7 trending coins on CoinGecko as searched by users in the last 24 hours (Ordered by most popular first).</para>
        /// </summary>
        /// <returns>A Task&lt;<see cref="SearchTrendingResponse"/>&gt; representing the asynchronous operation.</returns>
        public async Task<SearchTrendingResponse> GetSearchTrendingAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("search", "trending"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<SearchTrendingResponse>(jsonStr);
        }
    }
}
