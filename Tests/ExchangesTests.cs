namespace Tests
{
    public class ExchangesTests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        [Test]
        public async Task GetExchangesTest()
        {
            await Helpers.DoRateLimiting();

            var exchangesResult = await _apiClient.Exchanges.GetExchangesAsync();

            Assert.IsNotNull(exchangesResult);
            Assert.IsNotEmpty(exchangesResult);
        }
    }
}
