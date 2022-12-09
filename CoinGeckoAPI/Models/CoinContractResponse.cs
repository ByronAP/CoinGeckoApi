using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinGeckoAPI.Models
{
    public class CoinContractResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("asset_platform_id")]
        public string AssetPlatformId { get; set; }

        [JsonProperty("platforms")]
        public Dictionary<string, string> Platforms { get; set; }

        [JsonProperty("detail_platforms")]
        public Dictionary<string, CoinPlatformDetail> DetailPlatforms { get; set; }

        [JsonProperty("block_time_in_minutes")]
        public uint? BlockTimeInMinutes { get; set; }

        [JsonProperty("hashing_algorithm")]
        public object HashingAlgorithm { get; set; }

        [JsonProperty("categories")]
        public string[] Categories { get; set; }

        [JsonProperty("public_notice")]
        public object PublicNotice { get; set; }

        [JsonProperty("additional_notices")]
        public object[] AdditionalNotices { get; set; }

        [JsonProperty("localization")]
        public Dictionary<string, string> Localization { get; set; }

        [JsonProperty("description")]
        public Dictionary<string, string> Description { get; set; }

        [JsonProperty("links")]
        public CoinLinks Links { get; set; }

        [JsonProperty("image")]
        public CoinImage Image { get; set; }

        [JsonProperty("country_origin")]
        public string CountryOrigin { get; set; }

        [JsonProperty("genesis_date")]
        public DateTimeOffset? GenesisDate { get; set; }

        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }

        [JsonProperty("sentiment_votes_up_percentage")]
        public double? SentimentVotesUpPercentage { get; set; }

        [JsonProperty("sentiment_votes_down_percentage")]
        public double? SentimentVotesDownPercentage { get; set; }

        [JsonProperty("market_cap_rank")]
        public uint? MarketCapRank { get; set; }

        [JsonProperty("coingecko_rank")]
        public uint? CoingeckoRank { get; set; }

        [JsonProperty("coingecko_score")]
        public double? CoingeckoScore { get; set; }

        [JsonProperty("developer_score")]
        public double? DeveloperScore { get; set; }

        [JsonProperty("community_score")]
        public double? CommunityScore { get; set; }

        [JsonProperty("liquidity_score")]
        public double? LiquidityScore { get; set; }

        [JsonProperty("public_interest_score")]
        public double? PublicInterestScore { get; set; }

        [JsonProperty("market_data")]
        public CoinMarketData MarketData { get; set; }

        [JsonProperty("community_data")]
        public CoinCommunityData CommunityData { get; set; }

        [JsonProperty("developer_data")]
        public CoinDeveloperData DeveloperData { get; set; }

        [JsonProperty("public_interest_stats")]
        public CoinPublicInterestStats PublicInterestStats { get; set; }

        [JsonProperty("status_updates")]
        public object[] StatusUpdates { get; set; }

        [JsonProperty("last_updated")]
        public DateTimeOffset? LastUpdated { get; set; }

        [JsonProperty("tickers")]
        public CoinTicker[] Tickers { get; set; }
    }
}
