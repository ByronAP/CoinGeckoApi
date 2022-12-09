using CoinGeckoAPI.Properties;

namespace CoinGeckoAPI
{
    public static class Constants
    {
        /// <summary>
        /// The display name of the API provider.
        /// </summary>
        public static readonly string API_NAME = "CoinGecko";

        /// <summary>
        /// Actual rate limit is 10-50 RPM
        /// </summary>
        public static readonly uint API_RATE_LIMIT_MS = 6000;
        /// <summary>
        /// Time interval when the rate limit is reset.
        /// AKA: If a response is 429 then let the api cool down for this long.
        /// </summary>
        public static readonly uint API_RATE_LIMIT_RESET_MS = 61000;

        public static readonly string API_BASE_URL = "https://api.coingecko.com";
        public static readonly string API_PRO_BASE_URL = "https://pro-api.coingecko.com";
        public static readonly uint API_VERSION = 3;

        /// <summary>
        /// The API logo at 128 X 128 in PNG format.
        /// This is an embedded resource.
        /// </summary>
        public static readonly byte[] API_LOGO_128X128_PNG = Resources.coingecko_logo;
    }
}
