using CoinGeckoAPI.Models;
using CoinGeckoAPI.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    public class NftsImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal NftsImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="asset_platform_id">The asset platform identifier.</param>
        /// <param name="per_page">The per page.</param>
        /// <param name="page">The page.</param>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<NftListItem>> GetNftsListAsync(NftsListOrderBy order = NftsListOrderBy.None, string asset_platform_id = "", uint per_page = 100, uint page = 1)
        {
            if (page == 0) { page = 1; }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("nfts", "list"));
            if (order != NftsListOrderBy.None) { request.AddQueryParameter("order", order.ToString()); }
            if (!string.IsNullOrEmpty(asset_platform_id) && !string.IsNullOrWhiteSpace(asset_platform_id)) { request.AddQueryParameter("asset_platform_id", asset_platform_id); }
            if (per_page != 100) { request.AddQueryParameter("per_page", per_page); }
            if (page != 1) { request.AddQueryParameter("page", page); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<NftListItem[]>(jsonStr);
        }
    }
}
