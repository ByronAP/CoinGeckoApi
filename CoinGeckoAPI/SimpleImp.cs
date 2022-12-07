using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    /// <summary>
    /// This class implements the Simple API calls and can not be instantiated directly.
    /// Access these methods through an instance of the <see cref="CoinGeckoClient"/>, <see cref="CoinGeckoClient.Simple"/> field.
    /// </summary>
    public class SimpleImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal SimpleImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// Get the current price of any cryptocurrencies in any other supported currencies that you need.
        /// </summary>
        /// <param name="ids">id of coins. <see cref="CoinsImp.GetCoinsListAsync">Coins.GetCoinsListAsync</see></param>
        /// <param name="vs_currencies">vs_currencies of coins. <see cref="SimpleImp.GetSupportedVSCurrenciesAsync">Simple.GetSupportedVSCurrenciesAsync</see></param>
        /// <param name="include_market_cap">true/false to include market_cap, default: false</param>
        /// <param name="include_24hr_vol">true/false to include 24hr_vol, default: false</param>
        /// <param name="include_24hr_change">true/false to include 24hr_change, default: false</param>
        /// <param name="include_last_updated_at">true/false to include last_updated_at of price, default: false</param>
        /// <param name="precision">any value 0 through 18 to specify decimal place for currency price value, default: 2</param>
        /// <returns>price(s) of cryptocurrency</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<Dictionary<string, Dictionary<string, decimal>>> GetPriceAsync(IEnumerable<string> ids, IEnumerable<string> vs_currencies, bool include_market_cap = false, bool include_24hr_vol = false, bool include_24hr_change = false, bool include_last_updated_at = false, uint precision = 2)
        {
            if (precision >= 18)
            {
                throw new ArgumentOutOfRangeException(nameof(precision), "Value must be 0 through 18");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("simple", "price"));
            request.AddQueryParameter("ids", String.Join(",", ids));
            request.AddQueryParameter("vs_currencies", String.Join(",", vs_currencies));
            if (include_market_cap) request.AddQueryParameter("include_market_cap", "true");
            if (include_24hr_vol) request.AddQueryParameter("include_24hr_vol", "true");
            if (include_24hr_change) request.AddQueryParameter("include_24hr_change", "true");
            if (include_last_updated_at) request.AddQueryParameter("include_last_updated_at", "true");
            if (precision != 2) request.AddQueryParameter("precision", precision);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, decimal>>>(jsonStr);
        }

        /// <summary>
        /// Get current price of tokens (using contract addresses) for a given platform in any other currency that you need.
        /// </summary>
        /// <param name="id">The id of the platform issuing tokens. <see cref="CoinGeckoClient.GetAssetPlatformsAsync">GetAssetPlatformsAsync</see></param>
        /// <param name="contract_addresses">The contract addresses of tokens.</param>
        /// <param name="vs_currencies">vs_currencies of coins. <see cref="SimpleImp.GetSupportedVSCurrenciesAsync">Simple.GetSupportedVSCurrenciesAsync</see></param>
        /// <param name="include_market_cap">true/false to include market_cap, default: false</param>
        /// <param name="include_24hr_vol">true/false to include 24hr_vol, default: false</param>
        /// <param name="include_24hr_change">true/false to include 24hr_change, default: false</param>
        /// <param name="include_last_updated_at">true/false to include last_updated_at of price, default: false</param>
        /// <param name="precision">any value 0 through 18 to specify decimal place for currency price value, default: 2</param>
        /// <returns>price(s) of cryptocurrency</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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
            if (include_market_cap) request.AddQueryParameter("include_market_cap", "true");
            if (include_24hr_vol) request.AddQueryParameter("include_24hr_vol", "true");
            if (include_24hr_change) request.AddQueryParameter("include_24hr_change", "true");
            if (include_last_updated_at) request.AddQueryParameter("include_last_updated_at", "true");
            if (precision != 2) request.AddQueryParameter("precision", precision);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, decimal>>>(jsonStr);
        }

        /// <summary>
        /// Get list of supported_vs_currencies.
        /// </summary>
        /// <returns>list of supported_vs_currencies</returns>
        public async Task<IEnumerable<string>> GetSupportedVSCurrenciesAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("simple", "supported_vs_currencies"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<string[]>(jsonStr);
        }
    }
}
