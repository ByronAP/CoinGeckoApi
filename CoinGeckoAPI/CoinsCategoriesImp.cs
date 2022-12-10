using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    public class CoinsCategoriesImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal CoinsCategoriesImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns>A Task&lt;CoinCategoriesResponse[]&gt; representing the asynchronous operation.</returns>
        public async Task<CoinCategoriesItem[]> GetCoinCategoriesAsync(CoinCategoriesOrderBy order = CoinCategoriesOrderBy.market_cap_desc)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", "categories"));
            if (order != CoinCategoriesOrderBy.market_cap_desc) { request.AddQueryParameter("order", order.ToString()); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinCategoriesItem[]>(jsonStr);
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <returns>A Task&lt;CoinCategoriesListItem[]&gt; representing the asynchronous operation.</returns>
        public async Task<CoinCategoriesListItem[]> GetCoinCategoriesListAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", "categories", "list"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinCategoriesListItem[]>(jsonStr);
        }
    }
}
