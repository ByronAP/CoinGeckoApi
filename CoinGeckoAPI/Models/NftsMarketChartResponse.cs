using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class NftsMarketChartResponse
    {
        [JsonProperty("floor_price_usd")]
        public double[][] FloorPriceUsd { get; set; }

        [JsonProperty("floor_price_native")]
        public double[][] FloorPriceNative { get; set; }

        [JsonProperty("h24_volume_usd")]
        public double[][] H24VolumeUsd { get; set; }

        [JsonProperty("h24_volume_native")]
        public double[][] H24VolumeNative { get; set; }

        [JsonProperty("market_cap_usd")]
        public double[][] MarketCapUsd { get; set; }

        [JsonProperty("market_cap_native")]
        public double[][] MarketCapNative { get; set; }
    }
}
