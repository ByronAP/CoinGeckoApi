using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinGeckoAPI.Models
{
    public class CoinLinks
    {
        [JsonProperty("homepage")]
        public string[] Homepage { get; set; }

        [JsonProperty("blockchain_site")]
        public string[] BlockchainSite { get; set; }

        [JsonProperty("official_forum_url")]
        public string[] OfficialForumUrl { get; set; }

        [JsonProperty("chat_url")]
        public string[] ChatUrl { get; set; }

        [JsonProperty("announcement_url")]
        public string[] AnnouncementUrl { get; set; }

        [JsonProperty("twitter_screen_name")]
        public string TwitterScreenName { get; set; }

        [JsonProperty("facebook_username")]
        public string FacebookUsername { get; set; }

        [JsonProperty("bitcointalk_thread_identifier")]
        public object BitcointalkThreadIdentifier { get; set; }

        [JsonProperty("telegram_channel_identifier")]
        public string TelegramChannelIdentifier { get; set; }

        [JsonProperty("subreddit_url")]
        public string SubredditUrl { get; set; }

        [JsonProperty("repos_url")]
        public Dictionary<string, string[]> ReposUrl { get; set; }
    }
}
