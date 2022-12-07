﻿using Newtonsoft.Json;

namespace CoinGeckoAPI.Models
{
    public class CoinsMarketRoi
    {
        [JsonProperty("times")]
        public decimal Times { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("percentage")]
        public decimal Percentage { get; set; }
    }
}
