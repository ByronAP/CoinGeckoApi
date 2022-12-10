using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
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
        /// TODO: Document this.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>SearchResponse.</returns>
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
    }
}
