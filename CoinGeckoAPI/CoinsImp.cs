using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    /// <summary>
    /// This class implements the Coins API calls and can not be instantiated directly.
    /// Access these methods through an instance of the <see cref="CoinGeckoClient"/>, <see cref="CoinGeckoClient.Coins"/> field.
    /// </summary>
    public class CoinsImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal CoinsImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// Get coins list as an asynchronous operation.
        /// </summary>
        /// <param name="include_platform">if set to <c>true</c> [include platform].</param>
        /// <returns>A Task&lt;IEnumerable`1&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<CoinsListItem>> GetCoinsListAsync(bool include_platform = false)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", "list"));
            if (include_platform) { request.AddQueryParameter("include_platform", "true"); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinsListItem[]>(jsonStr);
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="vs_currency">The vs currency.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="category">The category.</param>
        /// <param name="order">The order.</param>
        /// <param name="per_page">The per page.</param>
        /// <param name="page">The page.</param>
        /// <param name="sparkline">if set to <c>true</c> [sparkline].</param>
        /// <param name="price_change_percentage">The price change percentage.</param>
        /// <returns>A Task&lt;CoinsMarketItem[]&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">vs_currency - Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">per_page - Must be a valid integer from 1 through 250.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">page - Must be a valid page index starting from 1.</exception>
        public async Task<CoinsMarketItem[]> GetMarketsAsync(string vs_currency, IEnumerable<string> ids = null, string category = "", MarketsOrderBy order = MarketsOrderBy.market_cap_desc, uint per_page = 100, uint page = 1, bool sparkline = false, MarketPriceChangePercentage price_change_percentage = MarketPriceChangePercentage.None)
        {
            if (string.IsNullOrEmpty(vs_currency) || vs_currency.Trim() == string.Empty)
            {
                throw new ArgumentNullException(nameof(vs_currency), "Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)");
            }

            if (per_page == 0 || per_page > 250)
            {
                throw new ArgumentOutOfRangeException(nameof(per_page), "Must be a valid integer from 1 through 250.");
            }

            if (page == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "Must be a valid page index starting from 1.");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", "markets"));
            request.AddQueryParameter("vs_currency", vs_currency);
            if (ids != null && ids.Any()) { request.AddQueryParameter("ids", String.Join(",", ids)); }
            if (!string.IsNullOrEmpty(category) && !string.IsNullOrWhiteSpace(category)) { request.AddQueryParameter("category", category); }
            request.AddQueryParameter("order", order.ToString().ToLowerInvariant());
            request.AddQueryParameter("per_page", per_page);
            request.AddQueryParameter("page", page);
            if (sparkline) { request.AddQueryParameter("sparkline", "true"); }

            if (!price_change_percentage.HasFlag(MarketPriceChangePercentage.None))
            {
                var marketPriceChangePercentageValues = new List<string>();

                if (price_change_percentage.HasFlag(MarketPriceChangePercentage.H1)) { marketPriceChangePercentageValues.Add("1h"); }
                if (price_change_percentage.HasFlag(MarketPriceChangePercentage.H24)) { marketPriceChangePercentageValues.Add("24h"); }
                if (price_change_percentage.HasFlag(MarketPriceChangePercentage.D7)) { marketPriceChangePercentageValues.Add("7d"); }
                if (price_change_percentage.HasFlag(MarketPriceChangePercentage.D14)) { marketPriceChangePercentageValues.Add("14d"); }
                if (price_change_percentage.HasFlag(MarketPriceChangePercentage.D30)) { marketPriceChangePercentageValues.Add("30d"); }
                if (price_change_percentage.HasFlag(MarketPriceChangePercentage.D200)) { marketPriceChangePercentageValues.Add("200d"); }
                if (price_change_percentage.HasFlag(MarketPriceChangePercentage.Y1)) { marketPriceChangePercentageValues.Add("1y"); }

                request.AddQueryParameter("price_change_percentage", string.Join(",", marketPriceChangePercentageValues));

            }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinsMarketItem[]>(jsonStr, new JsonSerializerSettings
            {
                Error = (sender, error) =>
                {
                    // sometimes a number has scientific notation or some other garbage so just ignore it
                    error.ErrorContext.Handled = true;
                }
            });
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="localization">if set to <c>true</c> [localization].</param>
        /// <param name="tickers">if set to <c>true</c> [tickers].</param>
        /// <param name="market_data">if set to <c>true</c> [market data].</param>
        /// <param name="community_data">if set to <c>true</c> [community data].</param>
        /// <param name="developer_data">if set to <c>true</c> [developer data].</param>
        /// <param name="sparkline">if set to <c>true</c> [sparkline].</param>
        /// <returns>A Task&lt;CoinResponse&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)</exception>
        public async Task<CoinResponse> GetCoinAsync(string id, bool localization = true, bool tickers = true, bool market_data = true, bool community_data = true, bool developer_data = true, bool sparkline = false)
        {
            if (string.IsNullOrEmpty(id) || id.Trim() == string.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", id));
            request.AddQueryParameter("localization", localization.ToString().ToLowerInvariant());
            request.AddQueryParameter("tickers", tickers.ToString().ToLowerInvariant());
            request.AddQueryParameter("market_data", market_data.ToString().ToLowerInvariant());
            request.AddQueryParameter("community_data", community_data.ToString().ToLowerInvariant());
            request.AddQueryParameter("developer_data", developer_data.ToString().ToLowerInvariant());
            request.AddQueryParameter("sparkline", sparkline.ToString().ToLowerInvariant());

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinResponse>(jsonStr);
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="exchange_ids">The exchange ids.</param>
        /// <param name="include_exchange_logo">if set to <c>true</c> [include exchange logo].</param>
        /// <param name="page">The page.</param>
        /// <param name="order">The order.</param>
        /// <param name="depth">if set to <c>true</c> [depth].</param>
        /// <returns>A Task&lt;CoinTickersResponse&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">page - Must be a valid page index starting from 1.</exception>
        public async Task<CoinTickersResponse> GetCoinTickersAsync(string id, IEnumerable<string> exchange_ids = null, bool include_exchange_logo = false, uint page = 1, CoinTickersOrderBy order = CoinTickersOrderBy.trust_score_desc, bool depth = false)
        {
            if (string.IsNullOrEmpty(id) || id.Trim() == string.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)");
            }

            if (page == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "Must be a valid page index starting from 1.");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", id, "tickers"));
            if (exchange_ids != null && exchange_ids.Any()) { request.AddQueryParameter("exchange_ids", string.Join(",", exchange_ids)); }
            if (include_exchange_logo) { request.AddQueryParameter("include_exchange_logo", "true"); }
            if (page != 1) { request.AddQueryParameter("page", page); }
            if (order != CoinTickersOrderBy.trust_score_desc) { request.AddQueryParameter("order", order.ToString()); }
            if (depth) { request.AddQueryParameter("depth", "true"); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinTickersResponse>(jsonStr);
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="date">The date.</param>
        /// <param name="localization">if set to <c>true</c> [localization].</param>
        /// <returns>A Task&lt;CoinHistoryResponse&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">date - Invalid value. Value must be a valid date to snapshot a coins history.</exception>
        public async Task<CoinHistoryResponse> GetCoinHistoryAsync(string id, DateTimeOffset date, bool localization = false)
        {
            if (string.IsNullOrEmpty(id) || id.Trim() == string.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)");
            }

            if (date == null || date == DateTimeOffset.MinValue || date == DateTimeOffset.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(date), "Invalid value. Value must be a valid date to snapshot a coins history.");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", id, "history"));
            request.AddQueryParameter("date", date.ToString("dd-MM-yyyy"));
            request.AddQueryParameter("localization", localization.ToString().ToLowerInvariant());

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinHistoryResponse>(jsonStr);
        }
    }
}
