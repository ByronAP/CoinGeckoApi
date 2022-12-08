using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinGeckoAPI.Models
{
    public class CoinTicker
    {
        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("target")]
        public string Target { get; set; }

        [JsonProperty("market")]
        public TickerMarketItem Market { get; set; }

        [JsonProperty("last")]
        public decimal? Last { get; set; }

        [JsonProperty("volume")]
        public decimal? Volume { get; set; }

        [JsonProperty("cost_to_move_up_usd")]
        public double? CostToMoveUpUsd { get; set; }

        [JsonProperty("cost_to_move_down_usd")]
        public double? CostToMoveDownUsd { get; set; }

        [JsonProperty("converted_last")]
        public Dictionary<string, decimal> ConvertedLast { get; set; }

        [JsonProperty("converted_volume")]
        public Dictionary<string, decimal> ConvertedVolume { get; set; }

        [JsonProperty("trust_score")]
        public string TrustScore { get; set; }

        [JsonProperty("bid_ask_spread_percentage")]
        public decimal? BidAskSpreadPercentage { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset? Timestamp { get; set; }

        [JsonProperty("last_traded_at")]
        public DateTimeOffset? LastTradedAt { get; set; }

        [JsonProperty("last_fetch_at")]
        public DateTimeOffset? LastFetchAt { get; set; }

        [JsonProperty("is_anomaly")]
        public bool? IsAnomaly { get; set; }

        [JsonProperty("is_stale")]
        public bool? IsStale { get; set; }

        [JsonProperty("trade_url")]
        public string TradeUrl { get; set; }

        [JsonProperty("token_info_url")]
        public string TokenInfoUrl { get; set; }

        [JsonProperty("coin_id")]
        public string CoinId { get; set; }

        [JsonProperty("target_coin_id", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetCoinId { get; set; }
    }
}
