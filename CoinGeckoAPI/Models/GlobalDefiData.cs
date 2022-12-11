using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class GlobalDefiData
    {
        [JsonProperty("defi_market_cap")]
        public string DefiMarketCap { get; set; }

        [JsonProperty("eth_market_cap")]
        public string EthMarketCap { get; set; }

        [JsonProperty("defi_to_eth_ratio")]
        public string DefiToEthRatio { get; set; }

        [JsonProperty("trading_volume_24h")]
        public string TradingVolume24H { get; set; }

        [JsonProperty("defi_dominance")]
        public string DefiDominance { get; set; }

        [JsonProperty("top_coin_name")]
        public string TopCoinName { get; set; }

        [JsonProperty("top_coin_defi_dominance")]
        public decimal? TopCoinDefiDominance { get; set; }
    }
}
