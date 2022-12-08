using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinGeckoAPI.Models
{
    public class CoinHistoryResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("localization")]
        public Dictionary<string, string> Localization { get; set; }

        [JsonProperty("image")]
        public CoinImage Image { get; set; }

        [JsonProperty("market_data")]
        public CoinMarketDataSlim MarketData { get; set; }

        [JsonProperty("community_data")]
        public CoinCommunityData CommunityData { get; set; }

        [JsonProperty("developer_data")]
        public CoinDeveloperData DeveloperData { get; set; }

        [JsonProperty("public_interest_stats")]
        public CoinPublicInterestStats PublicInterestStats { get; set; }
    }
}
