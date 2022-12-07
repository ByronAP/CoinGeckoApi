namespace Tests
{
    internal class CoinsTests
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

            Assert.IsNotNull(coinsResult);
            Assert.IsNotEmpty(coinsResult);

            var husdItem = coinsResult.FirstOrDefault(x => x.Id.Equals("husd", StringComparison.InvariantCultureIgnoreCase));

            Assert.IsNotNull(husdItem);
            Assert.IsNull(husdItem.Platforms);


            await Helpers.DoRateLimiting();

            coinsResult = await _apiClient.Coins.GetCoinsListAsync(true);

            Assert.IsNotNull(coinsResult);
            Assert.IsNotEmpty(coinsResult);

            husdItem = coinsResult.FirstOrDefault(x => x.Id.Equals("husd", StringComparison.InvariantCultureIgnoreCase));

            Assert.IsNotNull(husdItem);
            Assert.IsNotNull(husdItem.Platforms);
            Assert.IsNotEmpty(husdItem.Platforms);
        }

        [Test]
        public async Task GetCoinsMarketsTest()
        {
            await Helpers.DoRateLimiting();

            var marketsResult = await _apiClient.Coins.GetMarketsAsync("usd");

            Assert.IsNotNull(marketsResult);
            Assert.IsNotEmpty(marketsResult);
            Assert.IsNull(marketsResult[0].SparklineIn7D);
            Assert.That(marketsResult.Count, Is.EqualTo(100));

            await Helpers.DoRateLimiting();

            marketsResult = await _apiClient.Coins.GetMarketsAsync("usd", per_page: 200, sparkline: true);

            Assert.IsNotNull(marketsResult);
            Assert.IsNotEmpty(marketsResult);
            Assert.IsNotNull(marketsResult[0].SparklineIn7D);
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
    }
}
