using System;

namespace CoinGeckoAPI
{
    [Flags]
    public enum MarketPriceChangePercentage
    {
        None = 0,
        H1 = 1,
        H24 = 2,
        D7 = 4,
        D14 = 8,
        D30 = 16,
        D200 = 32,
        Y1 = 64
    }
}
