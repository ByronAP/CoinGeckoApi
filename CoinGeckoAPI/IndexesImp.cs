using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    public class IndexesImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal IndexesImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="per_page">The per page.</param>
        /// <param name="page">The page.</param>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<IndexItem>> GetIndexesAsync(uint per_page = 50, uint page = 1)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("indexes"));
            if (per_page != 50) { request.AddQueryParameter("per_page", per_page); }
            if (page != 0 && page != 1) { request.AddQueryParameter("page", page); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<IndexItem[]>(jsonStr);
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="market_id">The market identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>A Task&lt;IndexItem&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">market_id - Invalid value. Value must be a valid market id (EX: cme_futures)</exception>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid index id (EX: btc)</exception>
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

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            var result = JsonConvert.DeserializeObject<IndexItem>(jsonStr);

            result.Id = id;

            return result;
        }
    }
}
