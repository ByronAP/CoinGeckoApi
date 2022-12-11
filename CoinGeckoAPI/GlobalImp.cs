// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="GlobalImp.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    /// <summary>
    /// <para>Implementation of the '/global' API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class GlobalImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal GlobalImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// Get cryptocurrency global data (total_volume, total_market_cap, ongoing icos, etc)
        /// </summary>
        /// <returns>A Task&lt;<see cref="GlobalResponse"/>&gt; representing the asynchronous operation.</returns>
        public async Task<GlobalResponse> GetGlobalAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("global"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<GlobalResponse>(jsonStr);
        }

        /// <summary>
        /// Get Top 100 Cryptocurrency Global decentralized Finance(defi) data.
        /// </summary>
        /// <returns>A Task&lt;<see cref="GlobalDefiResponse"/>&gt; representing the asynchronous operation.</returns>
        public async Task<GlobalDefiResponse> GetGlobalDefiAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("global", "decentralized_finance_defi"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<GlobalDefiResponse>(jsonStr);
        }
    }
}
