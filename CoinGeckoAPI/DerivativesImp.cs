using CoinGeckoAPI.Models;
using CoinGeckoAPI.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    public class DerivativesImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal DerivativesImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="include_all">if set to <c>true</c> [include all].</param>
        /// <returns>IEnumerable&lt;DerivativesTickerItem&gt;.</returns>
        public async Task<IEnumerable<DerivativesTickerItem>> GetDerivativesAsync(bool include_all = false)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("derivatives"));
            if (include_all) { request.AddQueryParameter("include_tickers", "all"); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<DerivativesTickerItem[]>(jsonStr);
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="per_page">The per page.</param>
        /// <param name="page">The page.</param>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<DerivativesExchangeItem>> GetDerivativesExchangesAsync(DerivativesExchangeOrderBy order = DerivativesExchangeOrderBy.trade_volume_24h_btc_desc, uint per_page = 50, uint page = 1)
        {
            if (page == 0) { page = 1; }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("derivatives", "exchanges"));
            request.AddQueryParameter("order", order.ToString());
            if (per_page != 50) { request.AddQueryParameter("per_page", per_page); }
            if (page != 1) { request.AddQueryParameter("page", page); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<DerivativesExchangeItem[]>(jsonStr);
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="include_all_tickers">if set to <c>true</c> [include all tickers].</param>
        /// <returns>A Task&lt;DerivativesExchangeDetailItem&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid derivatives exchange (EX: zbg_futures)</exception>
        public async Task<DerivativesExchangeDetailItem> GetDerivativesExchangeAsync(string id, bool include_all_tickers = false)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid derivatives exchange (EX: zbg_futures)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("derivatives", "exchanges", id));
            request.AddQueryParameter("include_tickers", include_all_tickers ? "all" : "unexpired");

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<DerivativesExchangeDetailItem>(jsonStr);
        }
    }
}
