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

            Assert.That(exchangesResult, Is.Not.Null);
            Assert.That(exchangesResult, Is.Not.Empty);
        }

        [Test]
        public async Task GetExchangesListTest()
        {
            await Helpers.DoRateLimiting();

            var exchangesResult = await _apiClient.Exchanges.GetExchangesListAsync();

            Assert.That(exchangesResult, Is.Not.Null);
            Assert.That(exchangesResult, Is.Not.Empty);

            var gdaxItem = exchangesResult.First(x => x.Id.Equals("bitstamp", StringComparison.InvariantCultureIgnoreCase));

            Assert.That(gdaxItem, Is.Not.Null);
            Assert.That(gdaxItem.Name, Is.EqualTo("Bitstamp"));
        }

        [Test]
        public async Task GetExchangeTest()
        {
            await Helpers.DoRateLimiting();

            var exchangeResult = await _apiClient.Exchanges.GetExchangeAsync("gdax");

            Assert.That(exchangeResult, Is.Not.Null);
            Assert.That(exchangeResult.Tickers, Is.Not.Empty);
            Assert.That(exchangeResult.Name, Is.EqualTo("Coinbase Exchange"));
        }
    }
}
