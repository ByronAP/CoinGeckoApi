using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class GlobalResponse
    {
        [JsonProperty("data")]
        public GlobalData Data { get; set; }
    }
}
