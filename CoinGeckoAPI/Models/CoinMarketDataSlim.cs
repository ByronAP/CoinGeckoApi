using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinGeckoAPI.Models
{
    public class CoinMarketDataSlim
    {
        [JsonProperty("current_price")]
        public Dictionary<string, decimal> CurrentPrice { get; set; }

        [JsonProperty("market_cap")]
        public Dictionary<string, double> MarketCap { get; set; }

        [JsonProperty("total_volume")]
        public Dictionary<string, double> TotalVolume { get; set; }
    }
}
