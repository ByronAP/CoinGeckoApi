using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
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
    }
}
