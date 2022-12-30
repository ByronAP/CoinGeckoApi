namespace Tests
{
    public class CoinsTests
    {
        [Test]
        public async Task GetCoinsListTest()
        {
            try
            {
                var coinsResult = await Helpers.GetApiClient().Coins.GetCoinsListAsync();

                Assert.That(coinsResult, Is.Not.Null);
                Assert.That(coinsResult, Is.Not.Empty);

                var husdItem = coinsResult.FirstOrDefault(x => x.Id.Equals("husd", StringComparison.InvariantCultureIgnoreCase));

                Assert.That(husdItem, Is.Not.Null);
                Assert.That(husdItem.Platforms, Is.Null);

                coinsResult = await Helpers.GetApiClient().Coins.GetCoinsListAsync(true);

                Assert.That(coinsResult, Is.Not.Null);
                Assert.That(coinsResult, Is.Not.Empty);

                husdItem = coinsResult.FirstOrDefault(x => x.Id.Equals("husd", StringComparison.InvariantCultureIgnoreCase));

                Assert.That(husdItem, Is.Not.Null);
                Assert.That(husdItem.Platforms, Is.Not.Null);
                Assert.That(husdItem.Platforms, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinsMarketsTest()
        {
            try
            {
                var marketsResult = await Helpers.GetApiClient().Coins.GetCoinMarketsAsync("usd");

                Assert.That(marketsResult, Is.Not.Null);
                Assert.That(marketsResult, Is.Not.Empty);
                Assert.That(marketsResult.First().SparklineIn7D, Is.Null);
                Assert.That(marketsResult.Count, Is.EqualTo(100));

                marketsResult = await Helpers.GetApiClient().Coins.GetCoinMarketsAsync("usd", per_page: 200, sparkline: true);

                Assert.That(marketsResult, Is.Not.Null);
                Assert.That(marketsResult, Is.Not.Empty);
                Assert.That(marketsResult.First().SparklineIn7D, Is.Not.Null);
                Assert.That(marketsResult.Count, Is.EqualTo(200));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinTest()
        {
            try
            {
                var coinResult = await Helpers.GetApiClient().Coins.GetCoinAsync("bitcoin");

                Assert.That(coinResult, Is.Not.Null);
                Assert.That(coinResult.BlockTimeInMinutes, Is.EqualTo(10));

                coinResult = await Helpers.GetApiClient().Coins.GetCoinAsync("cosmos");

                Assert.That(coinResult, Is.Not.Null);
                Assert.That(coinResult.Symbol, Is.EqualTo("atom"));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinTickersTest()
        {
            try
            {
                var tickersResult = await Helpers.GetApiClient().Coins.GetCoinTickersAsync("bitcoin");

                Assert.That(tickersResult, Is.Not.Null);
                Assert.That(tickersResult.Tickers, Is.Not.Empty);

                tickersResult = await Helpers.GetApiClient().Coins.GetCoinTickersAsync("bitcoin", null, true, 1, CoinTickersOrderBy.trust_score_desc, true);

                Assert.That(tickersResult, Is.Not.Null);
                Assert.That(tickersResult.Tickers, Is.Not.Empty);
                Assert.That(tickersResult.Tickers[0].Market.Logo, Is.Not.Null);
                Assert.That(tickersResult.Tickers[0].Market.Logo, Is.Not.EqualTo(String.Empty));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinHistoryTest()
        {
            try
            {
                var historyResult = await Helpers.GetApiClient().Coins.GetCoinHistoryAsync("bitcoin", DateTimeOffset.UtcNow.AddDays(-2));

                Assert.That(historyResult, Is.Not.Null);
                Assert.That(historyResult.MarketData.CurrentPrice, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinMarketChartTest()
        {
            try
            {
                var chartResult = await Helpers.GetApiClient().Coins.GetCoinMarketChartAsync("bitcoin", "usd", 1000);

                Assert.That(chartResult, Is.Not.Null);
                Assert.That(chartResult.Prices, Is.Not.Empty);
                Assert.That(chartResult.MarketCaps, Is.Not.Empty);
                Assert.That(chartResult.TotalVolumes, Is.Not.Empty);

                var marketChartItems = chartResult.ToMarketChartCombinedItems();

                Assert.That(marketChartItems, Is.Not.Null);
                Assert.That(marketChartItems, Is.Not.Empty);
                Assert.That(marketChartItems.First().Price, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().MarketCap, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().TotalVolume, Is.GreaterThan(0));

                chartResult = await Helpers.GetApiClient().Coins.GetCoinMarketChartAsync("cosmos", "usd", 1000);

                Assert.That(chartResult, Is.Not.Null);
                Assert.That(chartResult.Prices, Is.Not.Empty);
                Assert.That(chartResult.MarketCaps, Is.Not.Empty);
                Assert.That(chartResult.TotalVolumes, Is.Not.Empty);

                marketChartItems = chartResult.ToMarketChartCombinedItems();

                Assert.That(marketChartItems, Is.Not.Null);
                Assert.That(marketChartItems, Is.Not.Empty);
                Assert.That(marketChartItems.First().Price, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().MarketCap, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().TotalVolume, Is.GreaterThan(0));

                chartResult = await Helpers.GetApiClient().Coins.GetCoinMarketChartAsync("ethereum", "usd", 1000);

                Assert.That(chartResult, Is.Not.Null);
                Assert.That(chartResult.Prices, Is.Not.Empty);
                Assert.That(chartResult.MarketCaps, Is.Not.Empty);
                Assert.That(chartResult.TotalVolumes, Is.Not.Empty);

                marketChartItems = chartResult.ToMarketChartCombinedItems();

                Assert.That(marketChartItems, Is.Not.Null);
                Assert.That(marketChartItems, Is.Not.Empty);
                Assert.That(marketChartItems.First().Price, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().MarketCap, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().TotalVolume, Is.GreaterThan(0));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinMarketChartRangeTest()
        {
            try
            {
                var chartResult = await Helpers.GetApiClient().Coins.GetCoinMarketChartRangeAsync("bitcoin", "usd", DateTimeOffset.UtcNow.AddMonths(-1), DateTimeOffset.UtcNow);

                Assert.That(chartResult, Is.Not.Null);
                Assert.That(chartResult.Prices, Is.Not.Empty);
                Assert.That(chartResult.MarketCaps, Is.Not.Empty);
                Assert.That(chartResult.TotalVolumes, Is.Not.Empty);

                var marketChartItems = chartResult.ToMarketChartCombinedItems();

                Assert.That(marketChartItems, Is.Not.Null);
                Assert.That(marketChartItems, Is.Not.Empty);
                Assert.That(marketChartItems.First().Price, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().MarketCap, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().TotalVolume, Is.GreaterThan(0));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinOhlcTest()
        {
            try
            {
                var ohlcResult = await Helpers.GetApiClient().Coins.GetCoinOhlcAsync("bitcoin", "usd", 14);

                Assert.That(ohlcResult, Is.Not.Null);
                Assert.That(ohlcResult, Is.Not.Empty);
                Assert.That(ohlcResult[0], Is.Not.Empty);
                Assert.That(ohlcResult[0][0], Is.GreaterThan(99999));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinOhlcItemsTest()
        {
            try
            {
                var ohlcResult = await Helpers.GetApiClient().Coins.GetCoinOhlcItemsAsync("bitcoin", "usd", 14);

                Assert.That(ohlcResult, Is.Not.Null);
                Assert.That(ohlcResult, Is.Not.Empty);
                Assert.That(ohlcResult.First(), Is.Not.Null);
                Assert.That(ohlcResult.First().Timestamp, Is.GreaterThan(99999));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }
    }
}
