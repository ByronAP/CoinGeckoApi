using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CoinGeckoAPI.Models
{
    public class CoinMarketData
    {
        [JsonProperty("current_price")]
        public Dictionary<string, decimal> CurrentPrice { get; set; }

        [JsonProperty("total_value_locked")]
        public double TotalValueLocked { get; set; }

        [JsonProperty("mcap_to_tvl_ratio")]
        public double McapToTvlRatio { get; set; }

        [JsonProperty("fdv_to_tvl_ratio")]
        public double FdvToTvlRatio { get; set; }

        [JsonProperty("roi")]
        public Roi Roi { get; set; }

        [JsonProperty("ath")]
        public Dictionary<string, decimal> Ath { get; set; }

        [JsonProperty("ath_change_percentage")]
        public Dictionary<string, double> AthChangePercentage { get; set; }

        [JsonProperty("ath_date")]
        public Dictionary<string, DateTimeOffset> AthDate { get; set; }

        [JsonProperty("atl")]
        public Dictionary<string, decimal> Atl { get; set; }

        [JsonProperty("atl_change_percentage")]
        public Dictionary<string, decimal> AtlChangePercentage { get; set; }

        [JsonProperty("atl_date")]
        public Dictionary<string, DateTimeOffset> AtlDate { get; set; }

        [JsonProperty("market_cap")]
        public Dictionary<string, double> MarketCap { get; set; }

        [JsonProperty("market_cap_rank")]
        public int MarketCapRank { get; set; }

        [JsonProperty("fully_diluted_valuation")]
        public Dictionary<string, double> FullyDilutedValuation { get; set; }

        [JsonProperty("total_volume")]
        public Dictionary<string, double> TotalVolume { get; set; }

        [JsonProperty("high_24h")]
        public Dictionary<string, decimal> High24H { get; set; }

        [JsonProperty("low_24h")]
        public Dictionary<string, decimal> Low24H { get; set; }

        [JsonProperty("price_change_24h")]
        public double PriceChange24H { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public double PriceChangePercentage24H { get; set; }

        [JsonProperty("price_change_percentage_7d")]
        public double PriceChangePercentage7D { get; set; }

        [JsonProperty("price_change_percentage_14d")]
        public double PriceChangePercentage14D { get; set; }

        [JsonProperty("price_change_percentage_30d")]
        public double PriceChangePercentage30D { get; set; }

        [JsonProperty("price_change_percentage_60d")]
        public double PriceChangePercentage60D { get; set; }

        [JsonProperty("price_change_percentage_200d")]
        public double PriceChangePercentage200D { get; set; }

        [JsonProperty("price_change_percentage_1y")]
        public double PriceChangePercentage1Y { get; set; }

        [JsonProperty("market_cap_change_24h")]
        public double MarketCapChange24H { get; set; }

        [JsonProperty("market_cap_change_percentage_24h")]
        public double MarketCapChangePercentage24H { get; set; }

        [JsonProperty("price_change_24h_in_currency")]
        public Dictionary<string, decimal> PriceChange24HInCurrency { get; set; }

        [JsonProperty("price_change_percentage_1h_in_currency")]
        public Dictionary<string, decimal> PriceChangePercentage1HInCurrency { get; set; }

        [JsonProperty("price_change_percentage_24h_in_currency")]
        public Dictionary<string, decimal> PriceChangePercentage24HInCurrency { get; set; }

        [JsonProperty("price_change_percentage_7d_in_currency")]
        public Dictionary<string, decimal> PriceChangePercentage7DInCurrency { get; set; }

        [JsonProperty("price_change_percentage_14d_in_currency")]
        public Dictionary<string, decimal> PriceChangePercentage14DInCurrency { get; set; }

        [JsonProperty("price_change_percentage_30d_in_currency")]
        public Dictionary<string, decimal> PriceChangePercentage30DInCurrency { get; set; }

        [JsonProperty("price_change_percentage_60d_in_currency")]
        public Dictionary<string, decimal> PriceChangePercentage60DInCurrency { get; set; }

        [JsonProperty("price_change_percentage_200d_in_currency")]
        public Dictionary<string, decimal> PriceChangePercentage200DInCurrency { get; set; }

        [JsonProperty("price_change_percentage_1y_in_currency")]
        public Dictionary<string, decimal> PriceChangePercentage1YInCurrency { get; set; }

        [JsonProperty("market_cap_change_24h_in_currency")]
        public Dictionary<string, decimal> MarketCapChange24HInCurrency { get; set; }

        [JsonProperty("market_cap_change_percentage_24h_in_currency")]
        public Dictionary<string, decimal> MarketCapChangePercentage24HInCurrency { get; set; }

        [JsonProperty("total_supply")]
        public double TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public double MaxSupply { get; set; }

        [JsonProperty("circulating_supply")]
        public double CirculatingSupply { get; set; }

        [JsonProperty("sparkline_7d")]
        public SparklineData Sparkline7D { get; set; }

        [JsonProperty("last_updated")]
        public DateTimeOffset LastUpdated { get; set; }
    }
}
