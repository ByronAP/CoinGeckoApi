// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="CompaniesImp.cs" company="ByronAP">
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
    /// <para>Implementation of the '/companies' API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class CompaniesImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal CompaniesImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }


        /// <summary>
        /// Get public companies bitcoin or ethereum holdings (Ordered by total holdings descending).
        /// <para>Only supports bitcoin and ethereum as currency. Default is bitcoin, set useETHCurrency for ethereum.</para>
        /// </summary>
        /// <param name="useETHCurrency">Set to <c>true</c> to use ethereum currency.</param>
        /// <returns>A Task&lt;<see cref="CompaniesPubTreasResponse"/>&gt; representing the asynchronous operation.</returns>
        public async Task<CompaniesPubTreasResponse> GetCompaniesPublicTreasuryAsync(bool useETHCurrency = false)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("companies", "public_treasury", useETHCurrency ? "ethereum" : "bitcoin"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CompaniesPubTreasResponse>(jsonStr);
        }
    }
}
