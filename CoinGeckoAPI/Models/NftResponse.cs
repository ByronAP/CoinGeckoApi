using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class NftResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }

        [JsonProperty("asset_platform_id")]
        public string AssetPlatformId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public CoinImage Image { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("native_currency")]
        public string NativeCurrency { get; set; }

        [JsonProperty("floor_price")]
        public NftPriceItem FloorPrice { get; set; }

        [JsonProperty("market_cap")]
        public NftPriceItem MarketCap { get; set; }

        [JsonProperty("volume_24h")]
        public NftPriceItem Volume24H { get; set; }

        [JsonProperty("floor_price_in_usd_24h_percentage_change")]
        public decimal? FloorPriceInUsd24HPercentageChange { get; set; }

        [JsonProperty("number_of_unique_addresses")]
        public long? NumberOfUniqueAddresses { get; set; }

        [JsonProperty("number_of_unique_addresses_24h_percentage_change")]
        public double? NumberOfUniqueAddresses24HPercentageChange { get; set; }

        [JsonProperty("total_supply")]
        public double? TotalSupply { get; set; }
    }
}
