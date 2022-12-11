using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class SearchResponse
    {
        [JsonProperty("coins")]
        public CoinSearchItem[] Coins { get; set; }

        [JsonProperty("exchanges")]
        public ExchangeSearchItem[] Exchanges { get; set; }

        // I have no idea what the structure of this is since
        // I can not find a search term that actually shows ICO
        [JsonProperty("icos")]
        public object[] Icos { get; set; }

        [JsonProperty("categories")]
        public IdNameListItem[] Categories { get; set; }

        [JsonProperty("nfts")]
        public NftSearchItem[] Nfts { get; set; }
    }
}
