using Microsoft.Extensions.Logging;

namespace Tests
{
    internal static class Helpers
    {
        private static CoinGeckoClient? _apiClient = null;

        private const uint _apiCallIntervalSeconds = 4;
        private static DateTimeOffset _lastCallAt = DateTimeOffset.MinValue;

        internal static async Task DoRateLimiting()
        {
            if (_lastCallAt.AddSeconds(_apiCallIntervalSeconds) > DateTimeOffset.UtcNow)
            {
                var waitTime = _lastCallAt.AddSeconds(_apiCallIntervalSeconds) - DateTimeOffset.UtcNow;
                await Task.Delay(TimeSpan.FromSeconds(waitTime.TotalSeconds));
            }

            _lastCallAt = DateTimeOffset.UtcNow;
        }

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
