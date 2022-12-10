using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CompaniesPubTreasResponse
    {
        [JsonProperty("total_holdings")]
        public decimal? TotalHoldings { get; set; }

        [JsonProperty("total_value_usd")]
        public decimal? TotalValueUsd { get; set; }

        [JsonProperty("market_cap_dominance")]
        public decimal? MarketCapDominance { get; set; }

        [JsonProperty("companies")]
        public CompanyInfo[] Companies { get; set; }
    }
}
