using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinCategoriesListItem
    {
        [JsonProperty("category_id")]
        public string CategoryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
