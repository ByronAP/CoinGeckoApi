// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="SimpleImp.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    /// <summary>
    /// This class implements the /simple API calls and can not be instantiated directly.
    /// Access these methods through an instance of the <see cref="CoinGeckoClient"/>, <see cref="CoinGeckoClient.Simple"/> field.
    /// </summary>
    public class SimpleImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;
        private readonly MemCache _cache;

        internal SimpleImp(RestClient restClient, MemCache cache, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _cache = cache;
            _restClient = restClient;
        }

        /// <summary>
        /// Get the current price of any cryptocurrencies in any other supported currencies that you need.
        /// </summary>
        /// <param name="ids">The ids of coins. <see cref="CoinsImp.GetCoinsListAsync"/></param>
        /// <param name="vs_currencies">The target currency of market data (usd, eur, jpy, etc.). See <see cref="SimpleImp.GetSupportedVSCurrenciesAsync"/>.</param>
        /// <param name="include_market_cap">Set to <c>true</c> to include market cap data in the response.</param>
        /// <param name="include_24hr_vol">Set to <c>true</c> to include 24HR vol data in the response.</param>
        /// <param name="include_24hr_change">Set to <c>true</c> to include 24HR change data in the response.</param>
        /// <param name="include_last_updated_at">Set to <c>true</c> to include last updated at value in response.</param>
        /// <param name="precision">Any value 0 through 18 to specify decimal place for currency price value. Default: 2.</param>
        /// <returns>A Task&lt;Dictionary&lt;string, Dictionary&lt;string, decimal&gt;&gt;&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">precision - Value must be 0 through 18</exception>
        public async Task<Dictionary<string, Dictionary<string, decimal>>> GetPriceAsync(IEnumerable<string> ids, IEnumerable<string> vs_currencies, bool include_market_cap = false, bool include_24hr_vol = false, bool include_24hr_change = false, bool include_last_updated_at = false, uint precision = 2)
        {
            if (precision >= 18)
            {
                throw new ArgumentOutOfRangeException(nameof(precision), "Value must be 0 through 18");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("simple", "price"));
            request.AddQueryParameter("ids", String.Join(",", ids));
            request.AddQueryParameter("vs_currencies", String.Join(",", vs_currencies));
            if (include_market_cap) { request.AddQueryParameter("include_market_cap", "true"); }
            if (include_24hr_vol) { request.AddQueryParameter("include_24hr_vol", "true"); }
            if (include_24hr_change) { request.AddQueryParameter("include_24hr_change", "true"); }
            if (include_last_updated_at) { request.AddQueryParameter("include_last_updated_at", "true"); }
            if (precision != 2) { request.AddQueryParameter("precision", precision); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, decimal>>>(jsonStr);
        }

        /// <summary>
        /// Get current price of tokens (using contract addresses) for a given platform in any other currency that you need.
        /// </summary>
        /// <param name="id">The id of the platform issuing tokens. <see cref="CoinGeckoClient.GetAssetPlatformsAsync"/>.</param>
        /// <param name="contract_addresses">The contract addresses of the tokens.</param>
        /// <param name="vs_currencies">The target currency of market data (usd, eur, jpy, etc.). See <see cref="SimpleImp.GetSupportedVSCurrenciesAsync"/>.</param>
        /// <param name="include_market_cap">Set to <c>true</c> to include market cap data in the response.</param>
        /// <param name="include_24hr_vol">Set to <c>true</c> to include 24HR vol data in the response.</param>
        /// <param name="include_24hr_change">Set to <c>true</c> to include 24HR change data in the response.</param>
        /// <param name="include_last_updated_at">Set to <c>true</c> to include last updated at value in the response.</param>
        /// <param name="precision">Any value 0 through 18 to specify decimal place for currency price value. Default: 2.</param>
        /// <returns>A Task&lt;Dictionary&lt;string, Dictionary&lt;string, decimal&gt;&gt;&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid id of the platform issuing tokens (See asset_platforms endpoint for list of options)</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">precision - Value must be 0 through 18</exception>
        public async Task<Dictionary<string, Dictionary<string, decimal>>> GetTokenPriceAsync(string id, IEnumerable<string> contract_addresses, IEnumerable<string> vs_currencies, bool include_market_cap = false, bool include_24hr_vol = false, bool include_24hr_change = false, bool include_last_updated_at = false, uint precision = 2)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid id of the platform issuing tokens (See asset_platforms endpoint for list of options)");
            }

            if (precision >= 18)
            {
                throw new ArgumentOutOfRangeException(nameof(precision), "Value must be 0 through 18");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("simple", "token_price", id));
            request.AddQueryParameter("contract_addresses", String.Join(",", contract_addresses));
            request.AddQueryParameter("vs_currencies", String.Join(",", vs_currencies));
            if (include_market_cap) { request.AddQueryParameter("include_market_cap", "true"); }
            if (include_24hr_vol) { request.AddQueryParameter("include_24hr_vol", "true"); }
            if (include_24hr_change) { request.AddQueryParameter("include_24hr_change", "true"); }
            if (include_last_updated_at) { request.AddQueryParameter("include_last_updated_at", "true"); }
            if (precision != 2) { request.AddQueryParameter("precision", precision); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, decimal>>>(jsonStr);
        }

        /// <summary>
        /// Get list of supported_vs_currencies.
        /// </summary>
        /// <returns>A Task&lt;IEnumerable&lt;string&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<string>> GetSupportedVSCurrenciesAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("simple", "supported_vs_currencies"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<string[]>(jsonStr);
        }
    }
}
