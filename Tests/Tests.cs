namespace Tests
{
    public class Tests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        [Test]
        public async Task PingTest()
        {
            await Helpers.DoRateLimiting();

            var pingResult = await _apiClient.PingAsync();

            Assert.IsNotNull(pingResult);
            Assert.IsTrue(pingResult);
        }

        [Test]
        public async Task GetExchangeRatesTest()
        {
            await Helpers.DoRateLimiting();

            var ratesResult = await _apiClient.GetExchangeRatesAsync();

            Assert.IsNotNull(ratesResult);
            Assert.IsNotNull(ratesResult.Rates);
            Assert.IsNotEmpty(ratesResult.Rates);
        }

        [Test]
        public async Task GetAssetPlatformsTest()
        {
            await Helpers.DoRateLimiting();

            var platformsResult = await _apiClient.GetAssetPlatformsAsync();

            Assert.IsNotNull(platformsResult);
            Assert.IsNotEmpty(platformsResult);
            Assert.That(platformsResult.Count(), Is.GreaterThanOrEqualTo(10));

            await Helpers.DoRateLimiting();

            platformsResult = await _apiClient.GetAssetPlatformsAsync("nft");

            Assert.IsNotNull(platformsResult);
            Assert.IsNotEmpty(platformsResult);
            Assert.That(platformsResult.Count(), Is.LessThanOrEqualTo(9));
        }
    }
}
