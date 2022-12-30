namespace Tests
{
    public class CoinsCategoriesTests
    {

        [Test]
        public async Task GetCoinCategoriesTest()
        {
            try
            {
                var categoriesResult = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

                Assert.That(categoriesResult, Is.Not.Null);
                Assert.That(categoriesResult, Is.Not.Empty);

                var ethItem = categoriesResult.First(x => x.Id.Equals("ethereum-ecosystem", StringComparison.InvariantCultureIgnoreCase));

                Assert.That(ethItem.Top3_Coins, Is.Not.Empty);
                Assert.That(ethItem.MarketCap, Is.GreaterThan(0));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinCategoriesListTest()
        {
            try
            {
                var categoriesResult = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesListAsync();

                Assert.That(categoriesResult, Is.Not.Null);
                Assert.That(categoriesResult, Is.Not.Empty);

                var ethItem = categoriesResult.First(x => x.CategoryId.Equals("ethereum-ecosystem", StringComparison.InvariantCultureIgnoreCase));

                Assert.That(ethItem, Is.Not.Null);
                Assert.That(ethItem.Name, Is.EqualTo("Ethereum Ecosystem"));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }
    }
}
