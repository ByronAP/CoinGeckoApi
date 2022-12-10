namespace Tests
{
    public class DerivativesTests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        [Test]
        public async Task GetDerivativesTest()
        {
            await Helpers.DoRateLimiting();

            var derivativesResult = await _apiClient.Derivatives.GetDerivativesAsync();

            Assert.That(derivativesResult, Is.Not.Null);
            Assert.That(derivativesResult, Is.Not.Empty);
        }

        [Test]
        public async Task GetDerivativesExchangesTest()
        {
            await Helpers.DoRateLimiting();

            var derivativesResult = await _apiClient.Derivatives.GetDerivativesExchangesAsync();

            Assert.That(derivativesResult, Is.Not.Null);
            Assert.That(derivativesResult, Is.Not.Empty);
        }
    }
}
