namespace Tests
{
    internal class CoinsContractTests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        [Test]
        public async Task GetCoinContractTest()
        {
            await Helpers.DoRateLimiting();

            var contractResult = await _apiClient.Coins.Contract.GetCoinContractAsync("ethereum", "0x514910771af9ca656af840dff83e8264ecf986ca");

            Assert.That(contractResult, Is.Not.Null);
            Assert.That(contractResult.Id, Is.EqualTo("chainlink"));
        }
    }
}
