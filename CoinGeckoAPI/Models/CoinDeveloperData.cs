using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinDeveloperData
    {
        [JsonProperty("forks")]
        public int Forks { get; set; }

        [JsonProperty("stars")]
        public int Stars { get; set; }

        [JsonProperty("subscribers")]
        public int Subscribers { get; set; }

        [JsonProperty("total_issues")]
        public int TotalIssues { get; set; }

        [JsonProperty("closed_issues")]
        public int ClosedIssues { get; set; }

        [JsonProperty("pull_requests_merged")]
        public int PullRequestsMerged { get; set; }

        [JsonProperty("pull_request_contributors")]
        public int PullRequestContributors { get; set; }

        [JsonProperty("code_additions_deletions_4_weeks")]
        public CoinDeveloperDataCodeAddDels CodeAdditionsDeletions4_Weeks { get; set; }

        [JsonProperty("commit_count_4_weeks")]
        public int CommitCount4_Weeks { get; set; }

        [JsonProperty("last_4_weeks_commit_activity_series")]
        public int[] Last4_WeeksCommitActivitySeries { get; set; }
    }
}
