namespace Tests
{
    public class IndexesTests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        [Test]
        public async Task GetIndexesTask()
        {
            await Helpers.DoRateLimiting();

            var categoriesResult = await _apiClient.Indexes.GetIndexesAsync();

            Assert.That(categoriesResult, Is.Not.Null);
            Assert.That(categoriesResult, Is.Not.Empty);
        }
    }
}
