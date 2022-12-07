using Newtonsoft.Json;
using System;

namespace CoinGeckoAPI.Models
{
    public class CoinsMarketItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("current_price")]
        public decimal CurrentPrice { get; set; }

        [JsonProperty("market_cap")]
        public long MarketCap { get; set; }

        [JsonProperty("market_cap_rank")]
        public long MarketCapRank { get; set; }

        [JsonProperty("fully_diluted_valuation")]
        public long? FullyDilutedValuation { get; set; }

        [JsonProperty("total_volume")]
        public long TotalVolume { get; set; }

        [JsonProperty("high_24h")]
        public decimal High24H { get; set; }

        [JsonProperty("low_24h")]
        public decimal Low24H { get; set; }

        [JsonProperty("price_change_24h")]
        public decimal PriceChange24H { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public decimal PriceChangePercentage24H { get; set; }

        [JsonProperty("market_cap_change_24h")]
        public decimal MarketCapChange24H { get; set; }

        [JsonProperty("market_cap_change_percentage_24h")]
        public decimal MarketCapChangePercentage24H { get; set; }

        [JsonProperty("circulating_supply")]
        public decimal CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public decimal? TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public long? MaxSupply { get; set; }

        [JsonProperty("ath")]
        public decimal Ath { get; set; }

        [JsonProperty("ath_change_percentage")]
        public decimal AthChangePercentage { get; set; }

        [JsonProperty("ath_date")]
        public DateTimeOffset AthDate { get; set; }

        [JsonProperty("atl")]
        public decimal Atl { get; set; }

        [JsonProperty("atl_change_percentage")]
        public decimal AtlChangePercentage { get; set; }

        [JsonProperty("atl_date")]
        public DateTimeOffset AtlDate { get; set; }

        [JsonProperty("roi")]
        public Roi Roi { get; set; }

        [JsonProperty("last_updated")]
        public DateTimeOffset LastUpdated { get; set; }

        [JsonProperty("sparkline_in_7d")]
        public SparklineData SparklineIn7D { get; set; }

        [JsonProperty("price_change_percentage_14d_in_currency")]
        public decimal PriceChangePercentage14DInCurrency { get; set; }

        [JsonProperty("price_change_percentage_1h_in_currency")]
        public decimal PriceChangePercentage1HInCurrency { get; set; }

        [JsonProperty("price_change_percentage_1y_in_currency")]
        public decimal PriceChangePercentage1YInCurrency { get; set; }

        [JsonProperty("price_change_percentage_200d_in_currency")]
        public decimal PriceChangePercentage200DInCurrency { get; set; }

        [JsonProperty("price_change_percentage_24h_in_currency")]
        public decimal PriceChangePercentage24HInCurrency { get; set; }

        [JsonProperty("price_change_percentage_30d_in_currency")]
        public decimal PriceChangePercentage30DInCurrency { get; set; }

        [JsonProperty("price_change_percentage_7d_in_currency")]
        public decimal PriceChangePercentage7DInCurrency { get; set; }
    }
}
