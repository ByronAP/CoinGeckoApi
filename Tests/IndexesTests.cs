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
        public async Task GetIndexesTest()
        {
            await Helpers.DoRateLimiting();

            var indexesResult = await _apiClient.Indexes.GetIndexesAsync();

            Assert.That(indexesResult, Is.Not.Null);
            Assert.That(indexesResult, Is.Not.Empty);
        }

        [Test]
        public async Task GetIndexTest()
        {
            await Helpers.DoRateLimiting();

            var indexResult = await _apiClient.Indexes.GetIndexAsync("cme_futures", "btc");

            Assert.That(indexResult, Is.Not.Null);
            Assert.That(indexResult.IsMultiAssetComposite, Is.False);
            Assert.That(indexResult.Name, Is.EqualTo("CME Bitcoin Futures BTC"));
        }

        [Test]
        public async Task GetIndexesListTest()
        {
            await Helpers.DoRateLimiting();

            var indexesResult = await _apiClient.Indexes.GetIndexesListAsync();

            Assert.That(indexesResult, Is.Not.Null);
            Assert.That(indexesResult, Is.Not.Empty);
        }
    }
}
