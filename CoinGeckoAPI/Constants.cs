namespace CoinGeckoAPI
{
    public static class Constants
    {
        /// <summary>
        /// The display name of the API provider.
        /// </summary>
        public const string API_NAME = "CoinGecko";

        /// <summary>
        /// Actual rate limit is 10-50 RPM
        /// </summary>
        public const uint API_RATE_LIMIT_MS = 6000;
        /// <summary>
        /// Time interval when the rate limit is reset.
        /// AKA: If a response is 429 then let the api cool down for this long.
        /// </summary>
        public const uint API_RATE_LIMIT_RESET_MS = 61000;

        public const string API_BASE_URL = "https://api.coingecko.com";
        public const string API_PRO_BASE_URL = "https://pro-api.coingecko.com";
        public const uint API_VERSION = 3;
    }
}
