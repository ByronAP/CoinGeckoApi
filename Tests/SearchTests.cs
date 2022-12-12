namespace Tests
{
    public class SearchTests
    {
        [Test]
        public async Task GetSearchTest()
        {
            await Helpers.DoRateLimiting();

            var searchResult = await Helpers.GetApiClient().Search.GetSearchAsync("8bit");

            Assert.That(searchResult, Is.Not.Null);
            Assert.That(searchResult.Coins, Is.Not.Empty);
            Assert.That(searchResult.Nfts, Is.Not.Empty);
            Assert.That(searchResult.Categories, Is.Not.Empty);

            await Helpers.DoRateLimiting();

            searchResult = await Helpers.GetApiClient().Search.GetSearchAsync("huobi");

            Assert.That(searchResult, Is.Not.Null);
            Assert.That(searchResult.Exchanges, Is.Not.Empty);
            Assert.That(searchResult.Coins, Is.Not.Empty);
            Assert.That(searchResult.Categories, Is.Not.Empty);
        }

        [Test]
        public async Task GetSearchTrendingTest()
        {
            await Helpers.DoRateLimiting();

            var searchResult = await Helpers.GetApiClient().Search.GetSearchTrendingAsync();

            Assert.That(searchResult, Is.Not.Null);
            Assert.That(searchResult.Coins, Is.Not.Empty);
        }
    }
}
