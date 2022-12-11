using System;

namespace CoinGeckoAPI.Types
{
    /// <summary>FLAGS, so you can use multiple at 1 time (<see cref="FlagsAttribute"/>).</summary>
    [Flags]
    public enum MarketPriceChangePercentage
    {
        None = 0,
        /// <summary>
        /// 1 Hour
        /// </summary>
        H1 = 1,
        /// <summary>
        /// 24 Hours (1 day)
        /// </summary>
        H24 = 2,
        /// <summary>
        /// 7 Days
        /// </summary>
        D7 = 4,
        /// <summary>
        /// 14 Days
        /// </summary>
        D14 = 8,
        /// <summary>
        /// 30 Days
        /// </summary>
        D30 = 16,
        /// <summary>
        /// 200 Days
        /// </summary>
        D200 = 32,
        /// <summary>
        /// 1 Year
        /// </summary>
        Y1 = 64
    }
}
