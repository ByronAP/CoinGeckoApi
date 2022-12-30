using CoinGeckoAPI;
using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CoinGeckoServiceCollectionExtensions
    {
        public static IServiceCollection AddCoinGeckoApi(this IServiceCollection services)
            => services.AddSingleton<CoinGeckoClient>();

        public static IServiceCollection AddCoinGeckoApi(this IServiceCollection services, string apiKey)
            => services.AddSingleton<CoinGeckoClient>(new CoinGeckoClient(apiKey: apiKey));

        public static IServiceCollection AddCoinGeckoApi(this IServiceCollection services, ILogger<CoinGeckoClient> logger)
            => services.AddSingleton<CoinGeckoClient>(new CoinGeckoClient(logger: logger));

        public static IServiceCollection AddCoinGeckoApi(this IServiceCollection services, string apiKey, ILogger<CoinGeckoClient> logger)
            => services.AddSingleton<CoinGeckoClient>(new CoinGeckoClient(apiKey: apiKey, logger: logger));
    }
}
