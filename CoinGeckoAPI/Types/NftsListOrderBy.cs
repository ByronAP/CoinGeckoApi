namespace CoinGeckoAPI.Types
{
    public enum NftsListOrderBy
    {
        None,
        /// <summary>
        /// Native currency volume 24 hours ascending
        /// </summary>
        h24_volume_native_asc,
        /// <summary>
        /// Native currency volume 24 hours descending
        /// </summary>
        h24_volume_native_desc,
        /// <summary>
        /// Native currency price floor ascending
        /// </summary>
        floor_price_native_asc,
        /// <summary>
        /// Native currency price floor descending
        /// </summary>
        floor_price_native_desc,
        /// <summary>
        /// Native currency market cap ascending
        /// </summary>
        market_cap_native_asc,
        /// <summary>
        /// Native currency market cap descending
        /// </summary>
        market_cap_native_desc,
        /// <summary>
        /// USD market cap ascending
        /// </summary>
        market_cap_usd_asc,
        /// <summary>
        /// USD market cap descending
        /// </summary>
        market_cap_usd_desc
    }
}
