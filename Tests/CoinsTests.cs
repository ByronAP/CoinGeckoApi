namespace Tests
{
    public class CoinsTests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        [Test]
        public async Task GetCoinsListTest()
        {
            await Helpers.DoRateLimiting();

            var coinsResult = await _apiClient.Coins.GetCoinsListAsync();

            Assert.That(coinsResult, Is.Not.Null);
            Assert.That(coinsResult, Is.Not.Empty);

            var husdItem = coinsResult.FirstOrDefault(x => x.Id.Equals("husd", StringComparison.InvariantCultureIgnoreCase));

            Assert.That(husdItem, Is.Not.Null);
            Assert.That(husdItem.Platforms, Is.Null);


            await Helpers.DoRateLimiting();

            coinsResult = await _apiClient.Coins.GetCoinsListAsync(true);

            Assert.That(coinsResult, Is.Not.Null);
            Assert.That(coinsResult, Is.Not.Empty);

            husdItem = coinsResult.FirstOrDefault(x => x.Id.Equals("husd", StringComparison.InvariantCultureIgnoreCase));

            Assert.That(husdItem, Is.Not.Null);
            Assert.That(husdItem.Platforms, Is.Not.Null);
            Assert.That(husdItem.Platforms, Is.Not.Empty);
        }

        [Test]
        public async Task GetCoinsMarketsTest()
        {
            await Helpers.DoRateLimiting();

            var marketsResult = await _apiClient.Coins.GetMarketsAsync("usd");

            Assert.That(marketsResult, Is.Not.Null);
            Assert.That(marketsResult, Is.Not.Empty);
            Assert.That(marketsResult.First().SparklineIn7D, Is.Null);
            Assert.That(marketsResult.Count, Is.EqualTo(100));

            await Helpers.DoRateLimiting();

            marketsResult = await _apiClient.Coins.GetMarketsAsync("usd", per_page: 200, sparkline: true);

            Assert.That(marketsResult, Is.Not.Null);
            Assert.That(marketsResult, Is.Not.Empty);
            Assert.That(marketsResult.First().SparklineIn7D, Is.Not.Null);
            Assert.That(marketsResult.Count, Is.EqualTo(200));
        }

        [Test]
        public async Task GetCoinTest()
        {
            await Helpers.DoRateLimiting();

            var coinResult = await _apiClient.Coins.GetCoinAsync("bitcoin");

            Assert.That(coinResult, Is.Not.Null);
            Assert.That(coinResult.BlockTimeInMinutes, Is.EqualTo(10));

            await Helpers.DoRateLimiting();

            coinResult = await _apiClient.Coins.GetCoinAsync("cosmos");

            Assert.That(coinResult, Is.Not.Null);
            Assert.That(coinResult.Symbol, Is.EqualTo("atom"));
        }

        [Test]
        public async Task GetCoinTickersTest()
        {
            await Helpers.DoRateLimiting();

            var tickersResult = await _apiClient.Coins.GetCoinTickersAsync("bitcoin");

            Assert.That(tickersResult, Is.Not.Null);
            Assert.That(tickersResult.Tickers, Is.Not.Empty);

            await Helpers.DoRateLimiting();

            tickersResult = await _apiClient.Coins.GetCoinTickersAsync("bitcoin", null, true, 1, CoinTickersOrderBy.trust_score_desc, true);

            Assert.That(tickersResult, Is.Not.Null);
            Assert.That(tickersResult.Tickers, Is.Not.Empty);
            Assert.That(tickersResult.Tickers[0].Market.Logo, Is.Not.Null);
            Assert.That(tickersResult.Tickers[0].Market.Logo, Is.Not.EqualTo(String.Empty));
        }

        [Test]
        public async Task GetCoinHistoryTest()
        {
            await Helpers.DoRateLimiting();

            var historyResult = await _apiClient.Coins.GetCoinHistoryAsync("bitcoin", DateTimeOffset.UtcNow.AddDays(-2));

            Assert.That(historyResult, Is.Not.Null);
            Assert.That(historyResult.MarketData.CurrentPrice, Is.Not.Empty);
        }

        [Test]
        public async Task GetCoinMarketChartTest()
        {
            await Helpers.DoRateLimiting();

            var chartResult = await _apiClient.Coins.GetCoinMarketChartAsync("bitcoin", "usd", 1000);

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

            await Helpers.DoRateLimiting();

            chartResult = await _apiClient.Coins.GetCoinMarketChartAsync("cosmos", "usd", 1000);

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

            await Helpers.DoRateLimiting();

            chartResult = await _apiClient.Coins.GetCoinMarketChartAsync("ethereum", "usd", 1000);

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

        [Test]
        public async Task GetCoinMarketChartRangeTest()
        {
            await Helpers.DoRateLimiting();

            var chartResult = await _apiClient.Coins.GetCoinMarketChartRangeAsync("bitcoin", "usd", DateTimeOffset.UtcNow.AddMonths(-1), DateTimeOffset.UtcNow);

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

        [Test]
        public async Task GetCoinOhlcTest()
        {
            await Helpers.DoRateLimiting();

            var ohlcResult = await _apiClient.Coins.GetCoinOhlcAsync("bitcoin", "usd", 14);

            Assert.That(ohlcResult, Is.Not.Null);
            Assert.That(ohlcResult, Is.Not.Empty);
            Assert.That(ohlcResult[0], Is.Not.Empty);
            Assert.That(ohlcResult[0][0], Is.GreaterThan(99999));
        }

        [Test]
        public async Task GetCoinOhlcItemsTest()
        {
            await Helpers.DoRateLimiting();

            var ohlcResult = await _apiClient.Coins.GetCoinOhlcItemsAsync("bitcoin", "usd", 14);

            Assert.That(ohlcResult, Is.Not.Null);
            Assert.That(ohlcResult, Is.Not.Empty);
            Assert.That(ohlcResult.First(), Is.Not.Null);
            Assert.That(ohlcResult.First().Timestamp, Is.GreaterThan(99999));
        }
    }
}
