using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class ExchangeTickersResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tickers")]
        public CoinTicker[] Tickers { get; set; }
    }
}
