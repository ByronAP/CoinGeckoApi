namespace Tests
{
    public class IndexesTests
    {
        [Test]
        public async Task GetIndexesTest()
        {
            try
            {
                var indexesResult = await Helpers.GetApiClient().Indexes.GetIndexesAsync();

                Assert.That(indexesResult, Is.Not.Null);
                Assert.That(indexesResult, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetIndexTest()
        {
            try
            {
                var indexResult = await Helpers.GetApiClient().Indexes.GetIndexAsync("cme_futures", "btc");

                Assert.That(indexResult, Is.Not.Null);
                Assert.That(indexResult.IsMultiAssetComposite, Is.False);
                Assert.That(indexResult.Name, Is.EqualTo("CME Bitcoin Futures BTC"));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetIndexesListTest()
        {
            try
            {
                var indexesResult = await Helpers.GetApiClient().Indexes.GetIndexesListAsync();

                Assert.That(indexesResult, Is.Not.Null);
                Assert.That(indexesResult, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }
    }
}
