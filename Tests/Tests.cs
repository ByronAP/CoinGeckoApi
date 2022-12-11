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

            Assert.That(pingResult, Is.True);
        }

        [Test]
        public async Task GetExchangeRatesTest()
        {
            await Helpers.DoRateLimiting();

            var ratesResult = await _apiClient.GetExchangeRatesAsync();

            Assert.That(ratesResult, Is.Not.Null);
            Assert.That(ratesResult.Rates, Is.Not.Null);
            Assert.That(ratesResult.Rates, Is.Not.Empty);
        }

        [Test]
        public async Task GetAssetPlatformsTest()
        {
            await Helpers.DoRateLimiting();

            var platformsResult = await _apiClient.GetAssetPlatformsAsync();

            Assert.That(platformsResult, Is.Not.Null);
            Assert.That(platformsResult, Is.Not.Empty);
            Assert.That(platformsResult.Count(), Is.GreaterThanOrEqualTo(10));

            await Helpers.DoRateLimiting();

            platformsResult = await _apiClient.GetAssetPlatformsAsync("nft");

            Assert.That(platformsResult, Is.Not.Null);
            Assert.That(platformsResult, Is.Not.Empty);
            Assert.That(platformsResult.Count(), Is.LessThanOrEqualTo(9));
        }

        [Test]
        public void LogoResourceTest()
        {
            var logoBytes = Constants.API_LOGO_128X128_PNG;

            Assert.That(logoBytes, Is.Not.Null);
            Assert.That(logoBytes, Is.Not.Empty);
        }
    }
}
