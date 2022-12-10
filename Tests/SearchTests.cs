namespace Tests
{
    public class SearchTests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        [Test]
        public async Task GetSearchTest()
        {
            await Helpers.DoRateLimiting();

            var searchResult = await _apiClient.Search.GetSearchAsync("8bit");

            Assert.That(searchResult, Is.Not.Null);
            Assert.That(searchResult.Coins, Is.Not.Empty);
            Assert.That(searchResult.Nfts, Is.Not.Empty);
            Assert.That(searchResult.Categories, Is.Not.Empty);

            await Helpers.DoRateLimiting();

            searchResult = await _apiClient.Search.GetSearchAsync("huobi");

            Assert.That(searchResult, Is.Not.Null);
            Assert.That(searchResult.Exchanges, Is.Not.Empty);
            Assert.That(searchResult.Coins, Is.Not.Empty);
            Assert.That(searchResult.Categories, Is.Not.Empty);
        }

        [Test]
        public async Task GetSearchTrendingTest()
        {
            await Helpers.DoRateLimiting();

            var searchResult = await _apiClient.Search.GetSearchTrendingAsync();

            Assert.That(searchResult, Is.Not.Null);
            Assert.That(searchResult.Coins, Is.Not.Empty);
        }
    }
}
