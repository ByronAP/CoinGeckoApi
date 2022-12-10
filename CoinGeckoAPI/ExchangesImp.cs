using CoinGeckoAPI.Models;
using CoinGeckoAPI.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    public class ExchangesImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal ExchangesImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="per_page">The per page.</param>
        /// <param name="page">The page.</param>
        /// <returns>A Task&lt;ExchangeItem[]&gt; representing the asynchronous operation.</returns>
        public async Task<ExchangeItem[]> GetExchangesAsync(uint per_page = 100, uint page = 1)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("exchanges"));
            if (per_page != 100) { request.AddQueryParameter("per_page", per_page); }
            if (page != 0 && page != 1) { request.AddQueryParameter("page", page); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<ExchangeItem[]>(jsonStr);
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <returns>A Task&lt;ExchangeListItem[]&gt; representing the asynchronous operation.</returns>
        public async Task<ExchangeListItem[]> GetExchangesListAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("exchanges", "list"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<ExchangeListItem[]>(jsonStr);
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ExchangeResponse> GetExchangeAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid exchange id (EX: bitstamp)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("exchanges", id));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            var result = JsonConvert.DeserializeObject<ExchangeResponse>(jsonStr);
            result.Id = id;
            return result;
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="coin_ids"></param>
        /// <param name="include_exchange_logo"></param>
        /// <param name="page"></param>
        /// <param name="depth"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ExchangeTickersResponse> GetExchangeTickersAsync(string id, IEnumerable<string> coin_ids, bool include_exchange_logo = false, uint page = 1, bool depth = false, CoinTickersOrderBy order = CoinTickersOrderBy.trust_score_desc)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid exchange id (EX: bitstamp)");
            }

            if (coin_ids == null || !coin_ids.Any())
            {
                throw new ArgumentNullException(nameof(coin_ids), "Invalid value. Value must be a valid enumerable of coin ids (EX: bitcoin, ethereum)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("exchanges", id, "tickers"));
            request.AddQueryParameter("coin_ids", String.Join(",", coin_ids));
            if (include_exchange_logo) { request.AddQueryParameter("include_exchange_logo", "true"); }
            if (page > 0) { request.AddQueryParameter("page", page); }
            if (depth) { request.AddQueryParameter("depth", depth); }
            if (order != CoinTickersOrderBy.trust_score_desc) { request.AddQueryParameter("order", order.ToString()); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<ExchangeTickersResponse>(jsonStr);
        }
    }
}
