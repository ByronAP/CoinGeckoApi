using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
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

    }
}
