using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinGeckoAPI.Models
{
    public class CoinMarketChartResponse
    {
        [JsonProperty("prices")]
        public decimal?[][] Prices { get; set; }

        [JsonProperty("market_caps")]
        public decimal?[][] MarketCaps { get; set; }

        [JsonProperty("total_volumes")]
        public decimal?[][] TotalVolumes { get; set; }

        /// <summary>
        /// Converts CoinMarketChartResponse to an easier to use format.
        /// In this combined type data is grouped together by date.
        /// </summary>
        /// <returns>IEnumerable&lt;MarketChartCombinedItem&gt;.</returns>
        public IEnumerable<MarketChartCombinedItem> ToMarketChartCombinedItems()
        {
            var results = new List<MarketChartCombinedItem>();

            foreach (var price in Prices)
            {
                var newItem = new MarketChartCombinedItem
                {
                    TimestampMs = Convert.ToInt64(price[0].Value),
                    Date = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(price[0].Value)),
                    Price = price[1].Value,
                    MarketCap = MarketCaps.FirstOrDefault(x => x[0].Value == price[0].Value)[1],
                    TotalVolume = TotalVolumes.FirstOrDefault(x => x[0].Value == price[0].Value)[1]
                };

                results.Add(newItem);
            }

            return results;
        }
    }
}
