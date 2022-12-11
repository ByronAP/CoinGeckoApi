using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class GlobalDefiResponse
    {
        [JsonProperty("data")]
        public GlobalDefiData Data { get; set; }
    }
}
