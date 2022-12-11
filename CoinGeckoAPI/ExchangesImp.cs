// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="ExchangesImp.cs" company="ByronAP">
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
    /// <para>Implementation of the '/exchanges' API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class ExchangesImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal ExchangesImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// List all exchanges (active with trading volumes).
        /// </summary>
        /// <param name="per_page">Total results per page. Default: 100.</param>
        /// <param name="page">Page through results</param>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="ExchangeItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">per_page - Invalid value. Value must be an integer from 1 to 250.</exception>
        public async Task<IEnumerable<ExchangeItem>> GetExchangesAsync(uint per_page = 100, uint page = 1)
        {
            if (per_page < 1 || per_page > 250)
            {
                throw new ArgumentOutOfRangeException(nameof(per_page), "Invalid value. Value must be an integer from 1 to 250.");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("exchanges"));
            if (per_page != 100) { request.AddQueryParameter("per_page", per_page); }
            if (page != 0 && page != 1) { request.AddQueryParameter("page", page); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<ExchangeItem[]>(jsonStr);
        }

        /// <summary>
        /// Use this to obtain all the markets' id in order to make API calls.
        /// </summary>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="IdNameListItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<IdNameListItem>> GetExchangesListAsync()
        {
            var request = new RestRequest(CoinGeckoClient.BuildUrl("exchanges", "list"));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<IdNameListItem[]>(jsonStr);
        }

        /// <summary>
        /// Get exchange volume in BTC and top 100 tickers only.
        /// <para>IMPORTANT:</para>
        /// <para>* Ticker object is limited to 100 items, to get more tickers, use <see cref="GetExchangeTickersAsync"/></para>
        /// <para>* Ticker <c>is_stale</c> is true when ticker that has not been updated/unchanged from the exchange for a while</para>
        /// <para>* Ticker <c>is_anomaly</c> is true if ticker's price is outliered by our system</para>
        /// <para>* You are responsible for managing how you want to display these information (e.g. footnote, different background, change opacity, hide)</para>
        /// </summary>
        /// <param name="id">The exchange id (can be obtained from <see cref="GetExchangesListAsync"/>).</param>
        /// <returns>A Task&lt;<see cref="ExchangeResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid exchange id (EX: bitstamp)</exception>
        public async Task<ExchangeResponse> GetExchangeAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid exchange id (EX: bitstamp)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("exchanges", id));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            var result = JsonConvert.DeserializeObject<ExchangeResponse>(jsonStr);
            result.Id = id;
            return result;
        }

        /// <summary>
        /// Get exchange tickers (paginated, 100 tickers per page).
        /// <para>* Ticker object is limited to 100 items per page</para>
        /// <para>* Ticker <c>is_stale</c> is true when ticker that has not been updated/unchanged from the exchange for a while</para>
        /// <para>* Ticker <c>is_anomaly</c> is true if ticker's price is outliered by our system</para>
        /// <para>* You are responsible for managing how you want to display these information (e.g. footnote, different background, change opacity, hide)</para>
        /// </summary>
        /// <param name="id">The exchange id (can be obtained from <see cref="GetExchangesListAsync"/>).</param>
        /// <param name="coin_ids">Filter tickers by coin_ids (<see cref="CoinsImp.GetCoinsListAsync"/>).</param>
        /// <param name="include_exchange_logo">Set to <c>true</c> to include exchange logo in response.</param>
        /// <param name="page">Page through results.</param>
        /// <param name="depth">Set to <c>true</c> to include 2% orderbook depth i.e., cost_to_move_up_usd and cost_to_move_down_usd.</param>
        /// <param name="order">The ordering of results (sorting) <see cref="CoinTickersOrderBy"/>. Default: trust_score_desc.</param>
        /// <returns>A Task&lt;<see cref="ExchangeTickersResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid exchange id (EX: bitstamp)</exception>
        /// <exception cref="System.ArgumentNullException">coin_ids - Invalid value. Value must be a valid enumerable of coin ids (EX: bitcoin, ethereum)</exception>
        public async Task<ExchangeTickersResponse> GetExchangeTickersAsync(string id, IEnumerable<string> coin_ids, bool include_exchange_logo = false, uint page = 1, bool depth = false, CoinTickersOrderBy order = CoinTickersOrderBy.trust_score_desc)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid exchange id (EX: bitstamp)");
            }

            if (coin_ids == null || !coin_ids.Any())
            {
                throw new ArgumentNullException(nameof(coin_ids), "Invalid value. Value must be a valid enumerable of coin ids (EX: bitcoin, ethereum)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("exchanges", id, "tickers"));
            request.AddQueryParameter("coin_ids", String.Join(",", coin_ids));
            if (include_exchange_logo) { request.AddQueryParameter("include_exchange_logo", "true"); }
            if (page > 0) { request.AddQueryParameter("page", page); }
            if (depth) { request.AddQueryParameter("depth", depth); }
            if (order != CoinTickersOrderBy.trust_score_desc) { request.AddQueryParameter("order", order.ToString()); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<ExchangeTickersResponse>(jsonStr);
        }

        /// <summary>
        /// Get volume_chart data for a given exchange.
        /// <para>This is the same as <see cref="GetExchangeVolumeChartAsync"/>, however the response is formatted in <see cref="ExchangeVolumeChartItem"/> items.</para>
        /// </summary>
        /// <param name="id">The exchange id (can be obtained from <see cref="GetExchangesListAsync"/>).</param>
        /// <param name="days">Data up to number of days ago (eg. 1,14,30).</param>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="ExchangeVolumeChartItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid exchange id (EX: bitstamp)</exception>
        public async Task<IEnumerable<ExchangeVolumeChartItem>> GetExchangeVolumeChartFriendlyAsync(string id, uint days)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid exchange id (EX: bitstamp)");
            }

            if (days == 0) { days = 1; }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("exchanges", id, "volume_chart"));
            request.AddQueryParameter("days", days);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            var data = JsonConvert.DeserializeObject<string[][]>(jsonStr);

            // the format of data suxxx so we automatically make it more easily used
            // by transforming it into concrete class enumerable
            var result = new List<ExchangeVolumeChartItem>();
            foreach (var item in data)
            {
                var newItem = new ExchangeVolumeChartItem
                {
                    Timestamp = long.Parse(item[0].Substring(0, item[0].IndexOf('.'))),
                    volume = decimal.Parse(item[1])
                };

                result.Add(newItem);
            }

            return result;
        }

        /// <summary>
        /// Get volume_chart data for a given exchange.
        /// <para>
        /// The results of this are not very user friendly (but good for charting).
        /// For a more user friendly data format see <see cref="GetExchangeVolumeChartFriendlyAsync"/>.
        /// </para>
        /// </summary>
        /// <param name="id">The exchange id (can be obtained from <see cref="GetExchangesListAsync"/>).</param>
        /// <param name="days">Data up to number of days ago (eg. 1,14,30).</param>
        /// <returns>A Task&lt;System.String[][]&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid exchange id (EX: bitstamp)</exception>
        public async Task<string[][]> GetExchangeVolumeChartAsync(string id, uint days)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid exchange id (EX: bitstamp)");
            }

            if (days == 0) { days = 1; }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("exchanges", id, "volume_chart"));
            request.AddQueryParameter("days", days);

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<string[][]>(jsonStr);
        }
    }
}
