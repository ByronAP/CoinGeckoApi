namespace Tests
{
    public class SearchTests
    {
        [Test]
        public async Task GetSearchTest()
        {
            try
            {
                var searchResult = await Helpers.GetApiClient().Search.GetSearchAsync("8bit");

                Assert.That(searchResult, Is.Not.Null);
                Assert.That(searchResult.Coins, Is.Not.Empty);
                Assert.That(searchResult.Nfts, Is.Not.Empty);
                Assert.That(searchResult.Categories, Is.Not.Empty);

                searchResult = await Helpers.GetApiClient().Search.GetSearchAsync("huobi");

                Assert.That(searchResult, Is.Not.Null);
                Assert.That(searchResult.Exchanges, Is.Not.Empty);
                Assert.That(searchResult.Coins, Is.Not.Empty);
                Assert.That(searchResult.Categories, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetSearchTrendingTest()
        {
            try
            {
                var searchResult = await Helpers.GetApiClient().Search.GetSearchTrendingAsync();

                Assert.That(searchResult, Is.Not.Null);
                Assert.That(searchResult.Coins, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }
    }
}
