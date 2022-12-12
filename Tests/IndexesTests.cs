namespace Tests
{
    public class IndexesTests
    {
        [Test]
        public async Task GetIndexesTest()
        {
            var indexesResult = await Helpers.GetApiClient().Indexes.GetIndexesAsync();

            Assert.That(indexesResult, Is.Not.Null);
            Assert.That(indexesResult, Is.Not.Empty);
        }

        [Test]
        public async Task GetIndexTest()
        {
            var indexResult = await Helpers.GetApiClient().Indexes.GetIndexAsync("cme_futures", "btc");

            Assert.That(indexResult, Is.Not.Null);
            Assert.That(indexResult.IsMultiAssetComposite, Is.False);
            Assert.That(indexResult.Name, Is.EqualTo("CME Bitcoin Futures BTC"));
        }

        [Test]
        public async Task GetIndexesListTest()
        {
            var indexesResult = await Helpers.GetApiClient().Indexes.GetIndexesListAsync();

            Assert.That(indexesResult, Is.Not.Null);
            Assert.That(indexesResult, Is.Not.Empty);
        }
    }
}
