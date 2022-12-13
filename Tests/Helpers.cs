using Microsoft.Extensions.Logging;

namespace Tests
{
    internal static class Helpers
    {
        private static CoinGeckoClient? _apiClient = null;

        internal static CoinGeckoClient GetApiClient()
        {
            if (_apiClient == null)
            {
                var factory = LoggerFactory.Create(x =>
                {
                    x.AddConsole();
                    x.SetMinimumLevel(LogLevel.Debug);
                });
                var logger = factory.CreateLogger<CoinGeckoClient>();

                _apiClient = new CoinGeckoClient(logger);
            }

            return _apiClient;
        }
    }
}
