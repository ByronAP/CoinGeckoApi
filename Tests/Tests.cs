namespace Tests
{
    public class Tests
    {
        [Test]
        public async Task InstantiateAndDisposeTest()
        {
            var apiClient = new CoinGeckoClient();

            var pingResult = await apiClient.PingAsync();

            Assert.That(pingResult, Is.True);

            apiClient.Dispose();

            Assert.Pass();

        }

        /// <summary>
        /// This test is not very accurate, find a better way.
        /// To test this, check the test output, it should show:
        /// * Cache Miss ...
        /// * Cache Hit ...
        /// * Cache Hit ...
        /// * Cache Hit ...
        /// * Cache Hit ...
        /// This indicates that a request was made to the remote host and then then the 
        /// other requests were served from our cache.
        /// </summary>
        [Test]
        public async Task CacheTest()
        {
            Helpers.GetApiClient().IsCacheEnabled = true;
            Helpers.GetApiClient().ClearCache();

            var categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            var updatedAt = categoriesResponse.First().UpdatedAt.Value;

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            // wait for the cache to clear, we don't just clear the cache because we want to make sure they are expiring
            await Task.Delay(300000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.Not.EqualTo(updatedAt));
        }

        [Test]
        public async Task CacheEnableDisableTest()
        {
            Helpers.GetApiClient().ClearCache();

            Helpers.GetApiClient().IsCacheEnabled = true;

            var categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            var updatedAt = categoriesResponse.First().UpdatedAt.Value;

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            Helpers.GetApiClient().IsCacheEnabled = false;

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            Helpers.GetApiClient().ClearCache();
            Helpers.GetApiClient().IsCacheEnabled = true;

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));
        }

        [Test]
        public async Task PingTest()
        {
            var pingResult = await Helpers.GetApiClient().PingAsync();

            Assert.That(pingResult, Is.True);
        }

        [Test]
        public async Task GetExchangeRatesTest()
        {
            var ratesResult = await Helpers.GetApiClient().GetExchangeRatesAsync();

            Assert.That(ratesResult, Is.Not.Null);
            Assert.That(ratesResult.Rates, Is.Not.Null);
            Assert.That(ratesResult.Rates, Is.Not.Empty);
        }

        [Test]
        public async Task GetAssetPlatformsTest()
        {
            var platformsResult = await Helpers.GetApiClient().GetAssetPlatformsAsync();

            Assert.That(platformsResult, Is.Not.Null);
            Assert.That(platformsResult, Is.Not.Empty);
            Assert.That(platformsResult.Count(), Is.GreaterThanOrEqualTo(10));

            platformsResult = await Helpers.GetApiClient().GetAssetPlatformsAsync("nft");

            Assert.That(platformsResult, Is.Not.Null);
            Assert.That(platformsResult, Is.Not.Empty);
            Assert.That(platformsResult.Count(), Is.LessThanOrEqualTo(9));
        }

        [Test]
        public void LogoResourceTest()
        {
            var logoBytes = Constants.API_LOGO_128X128_PNG;

            Assert.That(logoBytes, Is.Not.Null);
            Assert.That(logoBytes, Is.Not.Empty);
        }
    }
}
