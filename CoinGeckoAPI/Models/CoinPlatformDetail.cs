using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinPlatformDetail
    {
        [JsonProperty("decimal_place")]
        public int? DecimalPlace { get; set; }

        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }
    }
}
