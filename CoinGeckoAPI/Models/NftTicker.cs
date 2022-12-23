using Newtonsoft.Json;
using System;

namespace CoinGeckoAPI.Models
{
    public class NftTicker
    {
        [JsonProperty("floor_price_in_native_currency")]
        public decimal FloorPriceInNativeCurrency { get; set; }

        [JsonProperty("h24_volume_in_native_currency")]
        public decimal H24VolumeInNativeCurrency { get; set; }

        [JsonProperty("native_currency")]
        public string NativeCurrency { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("nft_marketplace_id")]
        public string NftMarketplaceId { get; set; }
    }
}
