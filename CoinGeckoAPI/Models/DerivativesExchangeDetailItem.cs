using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class DerivativesExchangeDetailItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("open_interest_btc")]
        public decimal? OpenInterestBtc { get; set; }

        [JsonProperty("trade_volume_24h_btc")]
        public decimal? TradeVolume24HBtc { get; set; }

        [JsonProperty("number_of_perpetual_pairs")]
        public int? NumberOfPerpetualPairs { get; set; }

        [JsonProperty("number_of_futures_pairs")]
        public int? NumberOfFuturesPairs { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("year_established")]
        public int? YearEstablished { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("tickers")]
        public DerivativesTicker[] Tickers { get; set; }
    }
}
