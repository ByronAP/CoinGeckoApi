namespace Tests
{
    public class Tests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        /// <summary>
        /// This test is not very accurate, find a better way.
        /// To test this, check the test output, it should show:
        /// * REMOTE
        /// * CACHE
        /// * CACHE
        /// * CACHE
        /// * CACHE
        /// This indicates that a request was made to the remote host and then then the 
        /// other requests were served from our cache.
        /// </summary>
        [Test]
        public async Task CacheTest()
        {
            var categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            var updatedAt = categoriesResponse.First().UpdatedAt.Value;

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            // wait for the cache to clear, we don't just clear the cache because we want to make sure they are expiring
            await Task.Delay(120000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.Not.EqualTo(updatedAt));

        }

        [Test]
        public async Task CacheEnableDisableTest()
        {
            await Helpers.DoRateLimiting();

            _apiClient.ClearCache();

            _apiClient.IsCacheEnabled = true;

            var categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            var updatedAt = categoriesResponse.First().UpdatedAt.Value;

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            _apiClient.IsCacheEnabled = false;

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            _apiClient.ClearCache();
            _apiClient.IsCacheEnabled = true;

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));

            await Task.Delay(1000);

            categoriesResponse = await _apiClient.Coins.Categories.GetCoinCategoriesAsync();

            Assert.IsNotNull(categoriesResponse);

            Assert.That(categoriesResponse, Is.Not.Empty);

            Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

            Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.EqualTo(updatedAt));
        }

        [Test]
        public async Task PingTest()
        {
            await Helpers.DoRateLimiting();

            var pingResult = await _apiClient.PingAsync();

            Assert.That(pingResult, Is.True);
        }

        [Test]
        public async Task GetExchangeRatesTest()
        {
            await Helpers.DoRateLimiting();

            var ratesResult = await _apiClient.GetExchangeRatesAsync();

            Assert.That(ratesResult, Is.Not.Null);
            Assert.That(ratesResult.Rates, Is.Not.Null);
            Assert.That(ratesResult.Rates, Is.Not.Empty);
        }

        [Test]
        public async Task GetAssetPlatformsTest()
        {
            await Helpers.DoRateLimiting();

            var platformsResult = await _apiClient.GetAssetPlatformsAsync();

            Assert.That(platformsResult, Is.Not.Null);
            Assert.That(platformsResult, Is.Not.Empty);
            Assert.That(platformsResult.Count(), Is.GreaterThanOrEqualTo(10));

            await Helpers.DoRateLimiting();

            platformsResult = await _apiClient.GetAssetPlatformsAsync("nft");

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
