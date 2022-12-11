using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    public class CompaniesImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal CompaniesImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }


        public async Task<CompaniesPubTreasResponse> GetCompaniesPublicTreasuryAsync(bool useETHCurrency = false)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("companies", "public_treasury", useETHCurrency ? "ethereum" : "bitcoin"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CompaniesPubTreasResponse>(jsonStr);
        }
    }
}
