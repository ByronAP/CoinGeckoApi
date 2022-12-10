using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
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
    }
}
