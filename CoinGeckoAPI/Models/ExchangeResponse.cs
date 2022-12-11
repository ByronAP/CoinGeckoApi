using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class ExchangeResponse
    {
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("year_established")]
        public uint? YearEstablished { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("facebook_url")]
        public string FacebookUrl { get; set; }

        [JsonProperty("reddit_url")]
        public string RedditUrl { get; set; }

        [JsonProperty("telegram_url")]
        public string TelegramUrl { get; set; }

        [JsonProperty("slack_url")]
        public string SlackUrl { get; set; }

        [JsonProperty("other_url_1")]
        public string OtherUrl1 { get; set; }

        [JsonProperty("other_url_2")]
        public string OtherUrl2 { get; set; }

        [JsonProperty("twitter_handle")]
        public string TwitterHandle { get; set; }

        [JsonProperty("has_trading_incentive")]
        public bool? HasTradingIncentive { get; set; }

        [JsonProperty("centralized")]
        public bool? Centralized { get; set; }

        [JsonProperty("public_notice")]
        public string PublicNotice { get; set; }

        [JsonProperty("alert_notice")]
        public string AlertNotice { get; set; }

        [JsonProperty("trust_score")]
        public uint? TrustScore { get; set; }

        [JsonProperty("trust_score_rank")]
        public uint? TrustScoreRank { get; set; }

        [JsonProperty("trade_volume_24h_btc")]
        public decimal? TradeVolume24HBtc { get; set; }

        [JsonProperty("trade_volume_24h_btc_normalized")]
        public decimal TradeVolume24HBtcNormalized { get; set; }

        [JsonProperty("tickers")]
        public CoinTicker[] Tickers { get; set; }

        [JsonProperty("status_updates")]
        public object[] StatusUpdates { get; set; }
    }
}
