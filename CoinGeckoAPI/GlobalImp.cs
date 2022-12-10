using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
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
        /// TODO: Document this.
        /// </summary>
        /// <returns>A Task&lt;GlobalResponse&gt; representing the asynchronous operation.</returns>
        public async Task<GlobalResponse> GetGlobalAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("global"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<GlobalResponse>(jsonStr);
        }
    }
}
