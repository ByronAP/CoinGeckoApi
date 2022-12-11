using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class NftListItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("asset_platform_id")]
        public string AssetPlatformId { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
