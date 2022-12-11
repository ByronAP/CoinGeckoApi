using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class NftPriceItem
    {
        [JsonProperty("native_currency")]
        public decimal? NativeCurrency { get; set; }

        [JsonProperty("usd")]
        public decimal? Usd { get; set; }
    }
}
