using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class SparklineData
    {
        [JsonProperty("price")]
        public decimal[] Price { get; set; }
    }
}
