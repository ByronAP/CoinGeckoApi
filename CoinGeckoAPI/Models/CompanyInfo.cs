using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CompanyInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("total_holdings")]
        public int? TotalHoldings { get; set; }

        [JsonProperty("total_entry_value_usd")]
        public long? TotalEntryValueUsd { get; set; }

        [JsonProperty("total_current_value_usd")]
        public long? TotalCurrentValueUsd { get; set; }

        [JsonProperty("percentage_of_total_supply")]
        public decimal? PercentageOfTotalSupply { get; set; }
    }
}
