using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class DerivativesTickerItem
    {
        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("index_id")]
        public string IndexId { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("price_percentage_change_24h")]
        public decimal? PricePercentageChange24H { get; set; }

        [JsonProperty("contract_type")]
        public string ContractType { get; set; }

        [JsonProperty("index")]
        public decimal? Index { get; set; }

        [JsonProperty("basis")]
        public decimal? Basis { get; set; }

        [JsonProperty("spread")]
        public decimal? Spread { get; set; }

        [JsonProperty("funding_rate")]
        public decimal? FundingRate { get; set; }

        [JsonProperty("open_interest")]
        public decimal? OpenInterest { get; set; }

        [JsonProperty("volume_24h")]
        public decimal Volume24H { get; set; }

        [JsonProperty("last_traded_at")]
        public long? LastTradedAt { get; set; }

        [JsonProperty("expired_at")]
        public long? ExpiredAt { get; set; }
    }
}
