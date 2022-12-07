using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinDeveloperDataCodeAddDels
    {
        [JsonProperty("additions")]
        public int Additions { get; set; }

        [JsonProperty("deletions")]
        public int Deletions { get; set; }
    }
}
