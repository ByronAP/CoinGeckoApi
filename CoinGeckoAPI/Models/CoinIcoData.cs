using Newtonsoft.Json;
using System;

namespace CoinGeckoAPI.Models
{
    public class CoinIcoData
    {
        [JsonProperty("ico_start_date")]
        public DateTimeOffset IcoStartDate { get; set; }

        [JsonProperty("ico_end_date")]
        public DateTimeOffset IcoEndDate { get; set; }

        [JsonProperty("short_desc")]
        public string ShortDesc { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("links")]
        public CoinIcoLinks Links { get; set; }

        [JsonProperty("softcap_currency")]
        public string SoftcapCurrency { get; set; }

        [JsonProperty("hardcap_currency")]
        public string HardcapCurrency { get; set; }

        [JsonProperty("total_raised_currency")]
        public string TotalRaisedCurrency { get; set; }

        [JsonProperty("softcap_amount")]
        public double SoftcapAmount { get; set; }

        [JsonProperty("hardcap_amount")]
        public double HardcapAmount { get; set; }

        [JsonProperty("total_raised")]
        public double TotalRaised { get; set; }

        [JsonProperty("quote_pre_sale_currency")]
        public string QuotePreSaleCurrency { get; set; }

        [JsonProperty("base_pre_sale_amount")]
        public string BasePreSaleAmount { get; set; }

        [JsonProperty("quote_pre_sale_amount")]
        public string QuotePreSaleAmount { get; set; }

        [JsonProperty("quote_public_sale_currency")]
        public string QuotePublicSaleCurrency { get; set; }

        [JsonProperty("base_public_sale_amount")]
        public double BasePublicSaleAmount { get; set; }

        [JsonProperty("quote_public_sale_amount")]
        public double QuotePublicSaleAmount { get; set; }

        [JsonProperty("accepting_currencies")]
        public string AcceptingCurrencies { get; set; }

        [JsonProperty("country_origin")]
        public string CountryOrigin { get; set; }

        [JsonProperty("pre_sale_start_date")]
        public DateTimeOffset PreSaleStartDate { get; set; }

        [JsonProperty("pre_sale_end_date")]
        public DateTimeOffset PreSaleEndDate { get; set; }

        [JsonProperty("whitelist_url")]
        public string WhitelistUrl { get; set; }

        [JsonProperty("whitelist_start_date")]
        public DateTimeOffset WhitelistStartDate { get; set; }

        [JsonProperty("whitelist_end_date")]
        public DateTimeOffset WhitelistEndDate { get; set; }

        [JsonProperty("bounty_detail_url")]
        public string BountyDetailUrl { get; set; }

        [JsonProperty("amount_for_sale")]
        public double AmountForSale { get; set; }

        [JsonProperty("kyc_required")]
        public bool KycRequired { get; set; }

        /// <summary>
        /// TODO: Could not find type.
        /// </summary>
        /// <value>The whitelist available.</value>
        [JsonProperty("whitelist_available")]
        public object WhitelistAvailable { get; set; }

        /// <summary>
        /// TODO: Could not find type.
        /// </summary>
        /// <value>The pre sale available.</value>
        [JsonProperty("pre_sale_available")]
        public object PreSaleAvailable { get; set; }

        [JsonProperty("pre_sale_ended")]
        public bool PreSaleEnded { get; set; }
    }
}
