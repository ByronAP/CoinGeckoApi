// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="CoinsImp.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
using CoinGeckoAPI.Models;
using CoinGeckoAPI.Types;
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
    /// <para>Implementation of the '/coins' API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class CoinsImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;
        private readonly MemCache _cache;

        public CoinsContractImp Contract { get; }
        public CoinsCategoriesImp Categories { get; }

        internal CoinsImp(RestClient restClient, MemCache cache, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _cache = cache;
            _restClient = restClient;

            Contract = new CoinsContractImp(restClient, _cache, logger);
            Categories = new CoinsCategoriesImp(restClient, _cache, logger);
        }

        /// <summary>
        /// Use this to obtain all the coins' id in order to make API calls.
        /// </summary>
        /// <param name="include_platform">Set to <c>true</c> to include platform contract addresses (eg. 0x.... for Ethereum based tokens) in the response.</param>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="CoinsListItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<CoinsListItem>> GetCoinsListAsync(bool include_platform = false)
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", "list"));
            if (include_platform) { request.AddQueryParameter("include_platform", "true"); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<CoinsListItem[]>(jsonStr);
        }

        /// <summary>
        /// Use this to obtain all the coins market data (price, market cap, volume).
        /// </summary>
        /// <param name="vs_currency">The target currency of market data (usd, eur, jpy, etc.).</param>
        /// <param name="ids">The ids of the coin cryptocurrency symbols (base). See <see cref="GetCoinsListAsync"/>.</param>
        /// <param name="category">Filter by coin category.</param>
        /// <param name="order">The order of the results (sort <see cref="MarketsOrderBy"/>).</param>
        /// <param name="per_page">Total results per page. Default value: 100.</param>
        /// <param name="page">Page through results. Default value: 1.</param>
        /// <param name="sparkline">Set to <c>true</c> to include sparkline 7 days data in the response.</param>
        /// <param name="price_change_percentage">Include price change percentage. These are flags so you can set as many as needed.</param>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="CoinsMarketItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">vs_currency - Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">per_page - Must be a valid integer from 1 through 250.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">page - Must be a valid page index starting from 1.</exception>
        public async Task<IEnumerable<CoinsMarketItem>> GetCoinMarketsAsync(string vs_currency, IEnumerable<string> ids = null, string category = "", MarketsOrderBy order = MarketsOrderBy.market_cap_desc, uint per_page = 100, uint page = 1, bool sparkline = false, MarketPriceChangePercentage price_change_percentage = MarketPriceChangePercentage.None)
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

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<CoinsMarketItem[]>(jsonStr, new JsonSerializerSettings
            {
                Error = (sender, error) =>
                {
                    // sometimes a number has scientific notation or some other garbage so just ignore it
                    // HACK: should fix this to parse the number
                    error.ErrorContext.Handled = true;
                }
            });
        }

        /// <summary>
        /// <para>Get current data (name, price, market, ... including exchange tickers) for a coin.</para>
        /// <para>IMPORTANT:</para>
        /// <para>Ticker object is limited to 100 items, to get more tickers, use <see cref="GetCoinTickersAsync"/>.
        /// Ticker is_stale is true when ticker has not been updated/unchanged from the exchange for a while.
        /// Ticker is_anomaly is true if ticker's price is outliered by our system.
        /// You are responsible for managing how you want to display these information(e.g.footnote, different background, change opacity, hide).</para>
        /// </summary>
        /// <param name="id">The id of the coin cryptocurrency. See <see cref="GetCoinsListAsync"/>.</param>
        /// <param name="localization">Set to <c>true</c> to include all localized languages in response.</param>
        /// <param name="tickers">Set to <c>true</c> to include tickers data in response.</param>
        /// <param name="market_data">Set to <c>true</c> to include market data in response.</param>
        /// <param name="community_data">Set to <c>true</c> to include community data in response.</param>
        /// <param name="developer_data">Set to <c>true</c> to include developer data in response.</param>
        /// <param name="sparkline">Set to <c>true</c> to include sparkline 7 dats data in response.</param>
        /// <returns>A Task&lt;<see cref="CoinResponse"/>&gt; representing the asynchronous operation.</returns>
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

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<CoinResponse>(jsonStr);
        }

        /// <summary>
        /// Get coin tickers.
        /// <para>IMPORTANT:</para>
        /// <para>Ticker object is limited to 100 items per page.
        /// Ticker is_stale is true when ticker has not been updated/unchanged from the exchange for a while.
        /// Ticker is_anomaly is true if ticker's price is outliered by our system.
        /// You are responsible for managing how you want to display these information(e.g.footnote, different background, change opacity, hide).</para>
        /// </summary>
        /// <param name="id">The id of the coin cryptocurrency. See <see cref="GetCoinsListAsync"/>.</param>
        /// <param name="exchange_ids">Filter results by exchange_ids (<see cref="ExchangesImp.GetExchangesListAsync"/>).</param>
        /// <param name="include_exchange_logo">if set to <c>true</c> include exchange logo in response.</param>
        /// <param name="page">Page through results.</param>
        /// <param name="order">The ordering of the results (sort <see cref="CoinTickersOrderBy"/>).</param>
        /// <param name="depth">Set to <c>true</c> to include 2% orderbook depth in the response.</param>
        /// <returns>A Task&lt;<see cref="CoinTickersResponse"/>&gt; representing the asynchronous operation.</returns>
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

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<CoinTickersResponse>(jsonStr);
        }

        /// <summary>
        /// Get historical data (name, price, market, stats) at a given date for a coin.
        /// </summary>
        /// <param name="id">The id of the coin cryptocurrency. See <see cref="GetCoinsListAsync"/>.</param>
        /// <param name="date">The date of data snapshot.</param>
        /// <param name="localization">Set to <c>true</c> to include all localized languages in response.</param>
        /// <returns>A Task&lt;<see cref="CoinHistoryResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">date - Invalid value. Value must be a valid date to snapshot a coins history.</exception>
        public async Task<CoinHistoryResponse> GetCoinHistoryAsync(string id, DateTimeOffset date, bool localization = false)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
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

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<CoinHistoryResponse>(jsonStr);
        }

        /// <summary>
        /// Get historical market data include price, market cap, and 24h volume (granularity auto).
        /// <para>* Data granularity is automatic (cannot be adjusted)</para>
        /// <para>* 1 day from current time = 5 minute interval data</para>
        /// <para>* 1 - 90 days from current time = hourly data</para>
        /// <para>* above 90 days from current time = daily data(00:00 UTC)</para>
        /// </summary>
        /// <param name="id">The id of the coin cryptocurrency. See <see cref="GetCoinsListAsync"/>.</param>
        /// <param name="vs_currency">The target currency of market data (usd, eur, jpy, etc.). See <see cref="SimpleImp.GetSupportedVSCurrenciesAsync"/>.</param>
        /// <param name="days">Data up to number of days ago (eg. 1,14,30,max).</param>
        /// <param name="interval">The interval (granularity <see cref="CoinMarketChartInterval"/>).</param>
        /// <returns>A Task&lt;<see cref="CoinMarketChartResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)</exception>
        /// <exception cref="System.ArgumentNullException">vs_currency - Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">days - Invalid value. Value must not exceed 900000.</exception>
        public async Task<CoinMarketChartResponse> GetCoinMarketChartAsync(string id, string vs_currency, uint days, CoinMarketChartInterval interval = CoinMarketChartInterval.auto)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)");
            }

            if (string.IsNullOrEmpty(vs_currency) || String.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(vs_currency), "Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)");
            }

            if (days > 900000)
            {
                throw new ArgumentOutOfRangeException(nameof(days), "Invalid value. Value must not exceed 900000.");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", id, "market_chart"));
            request.AddQueryParameter("vs_currency", vs_currency);
            request.AddQueryParameter("days", days);
            if (interval != CoinMarketChartInterval.auto) { request.AddQueryParameter("interval", interval.ToString().ToLowerInvariant()); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<CoinMarketChartResponse>(jsonStr);
        }

        /// <summary>
        /// Get historical market data include price, market cap, and 24h volume within a range of timestamp (granularity auto).
        /// <para>* Data granularity is automatic (cannot be adjusted)</para>
        /// <para>* 1 day from current time = 5 minute interval data</para>
        /// <para>* 1 - 90 days from current time = hourly data</para>
        /// <para>* above 90 days from current time = daily data(00:00 UTC)</para>
        /// </summary>
        /// <param name="id">The id of the coin cryptocurrency. See <see cref="GetCoinsListAsync"/>.</param>
        /// <param name="vs_currency">The target currency of market data (usd, eur, jpy, etc.). See <see cref="SimpleImp.GetSupportedVSCurrenciesAsync"/>.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>A Task&lt;<see cref="CoinMarketChartResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)</exception>
        /// <exception cref="System.ArgumentNullException">vs_currency - Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)</exception>
        public async Task<CoinMarketChartResponse> GetCoinMarketChartRangeAsync(string id, string vs_currency, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)");
            }

            if (string.IsNullOrEmpty(vs_currency) || String.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(vs_currency), "Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", id, "market_chart", "range"));
            request.AddQueryParameter("vs_currency", vs_currency);
            request.AddQueryParameter("from", fromDate.ToUnixTimeSeconds());
            request.AddQueryParameter("to", toDate.ToUnixTimeSeconds());

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<CoinMarketChartResponse>(jsonStr);
        }

        /// <summary>
        /// Get coin's OHLC (open, high, low, close) data (granularity auto).
        /// <para>* 1 - 2 days: 30 minutes</para>
        /// <para>* 3 - 30 days: 4 hours</para>
        /// <para>* 31 days and beyond: 4 days</para>
        /// </summary>
        /// <param name="id">The id of the coin cryptocurrency. See <see cref="GetCoinsListAsync"/>.</param>
        /// <param name="vs_currency">The target currency of market data (usd, eur, jpy, etc.). See <see cref="SimpleImp.GetSupportedVSCurrenciesAsync"/>.</param>
        /// <param name="days">Data up to number of days ago (1/7/14/30/90/180/365/max).</param>
        /// <returns>A Task&lt;System.Decimal[]&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)</exception>
        /// <exception cref="System.ArgumentNullException">vs_currency - Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)</exception>
        public async Task<decimal[][]> GetCoinOhlcAsync(string id, string vs_currency, uint days)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)");
            }

            if (string.IsNullOrEmpty(vs_currency) || String.IsNullOrWhiteSpace(vs_currency))
            {
                throw new ArgumentNullException(nameof(vs_currency), "Invalid value. Value must be a valid target currency of market data (usd, eur, jpy, etc.)");
            }

            if (days <= 0)
            {
                // since other endpoints accept 0 as 1 we will just make this act in the same way
                days = 1;
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", id, "ohlc"));
            request.AddQueryParameter("vs_currency", vs_currency);
            request.AddQueryParameter("days", days);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _cache, _logger);

            return JsonConvert.DeserializeObject<decimal[][]>(jsonStr);
        }

        /// <summary>
        /// Get coin's OHLC (open, high, low, close) data (granularity auto).
        /// <para>This is the same as <see cref="GetCoinOhlcAsync"/>, however the results are returned as concrete class items of <see cref="OhlcItem"/>.</para>
        /// <para>* 1 - 2 days: 30 minutes</para>
        /// <para>* 3 - 30 days: 4 hours</para>
        /// <para>* 31 days and beyond: 4 days</para>
        /// </summary>
        /// <param name="id">The id of the coin cryptocurrency. See <see cref="GetCoinsListAsync"/>.</param>
        /// <param name="vs_currency">The target currency of market data (usd, eur, jpy, etc.). See <see cref="SimpleImp.GetSupportedVSCurrenciesAsync"/>.</param>
        /// <param name="days">Data up to number of days ago (1/7/14/30/90/180/365/max).</param>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="OhlcItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<OhlcItem>> GetCoinOhlcItemsAsync(string id, string vs_currency, uint days)
        {
            var data = await GetCoinOhlcAsync(id, vs_currency, days);

            var result = new List<OhlcItem>();

            foreach (var item in data)
            {
                var newItem = new OhlcItem
                {
                    Timestamp = Convert.ToInt64(item[0]),
                    Open = item[1],
                    High = item[2],
                    Low = item[3],
                    Close = item[4]
                };

                result.Add(newItem);
            }

            return result;
        }
    }
}
