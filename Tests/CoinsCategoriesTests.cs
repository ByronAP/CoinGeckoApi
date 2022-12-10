namespace Tests
{
    public class CoinsCategoriesTests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        [Test]
        public async Task GetCoinCategoriesTest()
        {
            await Helpers.DoRateLimiting();

            var categoriesResult = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.That(categoriesResult, Is.Not.Null);
            Assert.That(categoriesResult, Is.Not.Empty);

            var ethItem = categoriesResult.First(x => x.Id.Equals("ethereum-ecosystem", StringComparison.InvariantCultureIgnoreCase));

            Assert.That(ethItem.Top3_Coins, Is.Not.Empty);
            Assert.That(ethItem.MarketCap, Is.GreaterThan(0));
        }

        [Test]
        public async Task GetCoinCategoriesListTest()
        {
            await Helpers.DoRateLimiting();

            var categoriesResult = await _apiClient.Coins.Categories.GetCoinCategoriesListAsync();

            Assert.That(categoriesResult, Is.Not.Null);
            Assert.That(categoriesResult, Is.Not.Empty);

            var ethItem = categoriesResult.First(x => x.CategoryId.Equals("ethereum-ecosystem", StringComparison.InvariantCultureIgnoreCase));

            Assert.That(ethItem, Is.Not.Null);
            Assert.That(ethItem.Name, Is.EqualTo("Ethereum Ecosystem"));
        }
    }
}
