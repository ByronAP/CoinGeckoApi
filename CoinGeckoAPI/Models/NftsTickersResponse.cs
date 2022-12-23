using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class NftsTickersResponse
    {
        [JsonProperty("tickers")]
        public NftTicker[] Tickers { get; set; }
    }
}
