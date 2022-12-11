using System;

namespace CoinGeckoAPI.Exceptions
{
    public class UnknownException : Exception
    {
        public UnknownException(string message) : base(message)
        {
        }
    }
}
