using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinGeckoAPI.Models
{
    public class GlobalData
    {
        [JsonProperty("active_cryptocurrencies")]
        public int? ActiveCryptocurrencies { get; set; }

        [JsonProperty("upcoming_icos")]
        public int? UpcomingIcos { get; set; }

        [JsonProperty("ongoing_icos")]
        public int? OngoingIcos { get; set; }

        [JsonProperty("ended_icos")]
        public int? EndedIcos { get; set; }

        [JsonProperty("markets")]
        public int? Markets { get; set; }

        [JsonProperty("total_market_cap")]
        public Dictionary<string, decimal> TotalMarketCap { get; set; }

        [JsonProperty("total_volume")]
        public Dictionary<string, decimal> TotalVolume { get; set; }

        [JsonProperty("market_cap_percentage")]
        public Dictionary<string, decimal> MarketCapPercentage { get; set; }

        [JsonProperty("market_cap_change_percentage_24h_usd")]
        public decimal? MarketCapChangePercentage24HUsd { get; set; }

        [JsonProperty("updated_at")]
        public long? UpdatedAt { get; set; }
    }
}
