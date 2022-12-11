using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinCommunityData
    {
        [JsonProperty("facebook_likes")]
        public int? FacebookLikes { get; set; }

        [JsonProperty("twitter_followers")]
        public int? TwitterFollowers { get; set; }

        [JsonProperty("reddit_average_posts_48h")]
        public double? RedditAveragePosts48H { get; set; }

        [JsonProperty("reddit_average_comments_48h")]
        public double? RedditAverageComments48H { get; set; }

        [JsonProperty("reddit_subscribers")]
        public int? RedditSubscribers { get; set; }

        // OBJECT is used here because this comes back as NULL, decimal, int, and string
        [JsonProperty("reddit_accounts_active_48h")]
        public object RedditAccountsActive48H { get; set; }

        [JsonProperty("telegram_channel_user_count")]
        public int? TelegramChannelUserCount { get; set; }
    }
}
