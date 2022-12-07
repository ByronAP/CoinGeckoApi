using CoinGeckoAPI.Exceptions;
using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    public class CoinGeckoClient
    {
        /// <summary>
        /// The RestSharp client instance.
        /// This is exposed in case you wish to change the options such as use a proxy.
        /// </summary>
        public RestClient CGRestClient { get; }

        /// <summary>
        /// Simple API calls.
        /// </summary>
        public SimpleImp Simple { get; }

        /// <summary>
        /// Coins API calls.
        /// </summary>
        public CoinsImp Coins { get; }

        private readonly ILogger<CoinGeckoClient> _logger;

        #region Constructors
        public CoinGeckoClient()
        {
            _logger = null;

            CGRestClient = new RestClient(Constants.API_BASE_URL);

            Simple = new SimpleImp(CGRestClient, _logger);
            Coins = new CoinsImp(CGRestClient, _logger);
        }

        public CoinGeckoClient(bool isPro)
        {
            _logger = null;

            if (isPro)
            {
                CGRestClient = new RestClient(Constants.API_PRO_BASE_URL);
            }
            else
            {
                CGRestClient = new RestClient(Constants.API_BASE_URL);
            }

            Simple = new SimpleImp(CGRestClient, _logger);
            Coins = new CoinsImp(CGRestClient, _logger);
        }

        public CoinGeckoClient(ILogger<CoinGeckoClient> logger)
        {
            _logger = logger;

            CGRestClient = new RestClient(Constants.API_BASE_URL);

            Simple = new SimpleImp(CGRestClient, _logger);
            Coins = new CoinsImp(CGRestClient, _logger);
        }

        public CoinGeckoClient(ILogger<CoinGeckoClient> logger, bool isPro)
        {
            _logger = logger;

            if (isPro)
            {
                CGRestClient = new RestClient(Constants.API_PRO_BASE_URL);
            }
            else
            {
                CGRestClient = new RestClient(Constants.API_BASE_URL);
            }

            Simple = new SimpleImp(CGRestClient, _logger);
            Coins = new CoinsImp(CGRestClient, _logger);
        }
        #endregion

        internal static async Task<string> GetStringResponseAsync(RestClient client, RestRequest request, ILogger logger)
        {
            try
            {
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return response.Content;
                }

                if (response.ErrorException != null)
                {
                    logger?.LogError(response.ErrorException, "GetStringResponseAsync failed.");
                    throw response.ErrorException;
                }

                throw new UnknownException($"Unknown exception, http response code is not success, {response.StatusCode}.");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "GetStringResponseAsync request failure.");
                throw;
            }
        }

        internal static string BuildUrl(params string[] parts)
        {
            if (parts.Length > 2)
            {
                var sb = new StringBuilder();
                sb.Append("/api/v").Append(Constants.API_VERSION);
                foreach (var part in parts)
                {
                    sb.Append('/');
                    sb.Append(part);
                }
                return sb.ToString();
            }
            else
            {
                var result = $"/api/v{Constants.API_VERSION}";
                foreach (var part in parts)
                {
                    result += $"/{part}";
                }
                return result;
            }
        }

        /// <summary>
        /// Check API server status.
        /// </summary>
        /// <returns>True if the api was successfully reached.</returns>
        public async Task<bool> PingAsync()
        {
            var request = new RestRequest(BuildUrl("ping"));

            try
            {
                var jsonString = await GetStringResponseAsync(CGRestClient, request, _logger);
                _logger?.LogDebug("{JsonString}", jsonString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get BTC-to-Currency exchange rates.
        /// </summary>
        /// <returns>List of rates.</returns>
        public async Task<ExchangeRatesResponse> GetExchangeRatesAsync()
        {
            var request = new RestRequest(BuildUrl("exchange_rates"));

            var jsonString = await GetStringResponseAsync(CGRestClient, request, _logger);

            return ExchangeRatesResponse.FromJson(jsonString);
        }

        /// <summary>
        /// List all asset platforms (Blockchain networks).
        /// </summary>
        /// <param name="filter">Apply relevant filters to results. Valid values: "nft" (asset_platform nft-support).</param>
        /// <returns>List all asset_platforms.</returns>
        public async Task<IEnumerable<AssetPlatform>> GetAssetPlatformsAsync(string filter = null)
        {
            var request = new RestRequest(BuildUrl("asset_platforms"));

            if (!string.IsNullOrEmpty(filter) && !string.IsNullOrWhiteSpace(filter))
            {
                request.AddQueryParameter("filter", filter);
            }

            var jsonString = await GetStringResponseAsync(CGRestClient, request, _logger);

            return JsonConvert.DeserializeObject<IEnumerable<AssetPlatform>>(jsonString);
        }
    }
}
