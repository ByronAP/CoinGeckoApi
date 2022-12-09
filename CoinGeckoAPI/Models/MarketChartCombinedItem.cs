using System;

namespace CoinGeckoAPI.Models
{
    public class MarketChartCombinedItem
    {
        public long TimestampMs { get; set; }

        public DateTimeOffset Date { get; set; }

        public decimal Price { get; set; }

        public decimal? MarketCap { get; set; }

        public decimal? TotalVolume { get; set; }
    }
}
