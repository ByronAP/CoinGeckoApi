using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinIcoLinks
    {
        [JsonProperty("web")]
        public string Web { get; set; }

        [JsonProperty("blog")]
        public string Blog { get; set; }

        [JsonProperty("slack")]
        public string Slack { get; set; }

        [JsonProperty("github")]
        public string Github { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("telegram")]
        public string Telegram { get; set; }

        [JsonProperty("whitepaper")]
        public string Whitepaper { get; set; }
    }
}
