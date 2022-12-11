using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinTickersResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tickers")]
        public CoinTicker[] Tickers { get; set; }
    }
}
