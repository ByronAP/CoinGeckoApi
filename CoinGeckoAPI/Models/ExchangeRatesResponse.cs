﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoinGeckoAPI.Models
{
    public class ExchangeRatesResponse
    {
        [JsonProperty("rates")]
        public Dictionary<string, ExchangeRate> Rates { get; set; }

        public static ExchangeRatesResponse FromJson(string json) => JsonConvert.DeserializeObject<ExchangeRatesResponse>(json);
    }
}
