// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-12-2022
// ***********************************************************************
// <copyright file="Constants.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
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
        /// <para>This is the wait time between calls to not ever get rate limited (AKA 10 RPM).</para>
        /// </summary>
        public static readonly uint API_RATE_LIMIT_MS = 6000;

        /// <summary>
        /// The absolute maximum number of api calls allowed in 1 minute (Requests Per Minute).
        /// </summary>
        public static readonly uint API_MAX_RPM = 50;

        /// <summary>
        /// Time interval when the rate limit is reset.
        /// AKA: If a response code is 429 then let the api cool down for this long.
        /// </summary>
        public static readonly uint API_RATE_LIMIT_RESET_MS = 90000;

        /// <summary>
        /// The base API URL.
        /// </summary>
        public static readonly string API_BASE_URL = "https://api.coingecko.com";

        /// <summary>
        /// The base URL for PRO feature calls.
        /// </summary>
        public static readonly string API_PRO_BASE_URL = "https://pro-api.coingecko.com";

        /// <summary>
        /// The API version.
        /// </summary>
        public static readonly uint API_VERSION = 3;

        /// <summary>
        /// The API logo at 128 X 128 in PNG format.
        /// This is an embedded resource.
        /// </summary>
        public static readonly byte[] API_LOGO_128X128_PNG = Resources.coingecko_logo;
    }
}
