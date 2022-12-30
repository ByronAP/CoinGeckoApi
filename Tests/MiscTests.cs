namespace Tests
{
    public class MiscTests
    {
        [Test]
        public async Task InstantiateAndDisposeTest()
        {
            try
            {
                var apiClient = new CoinGeckoClient();

                var pingResult = await apiClient.PingAsync();

                Assert.That(pingResult, Is.True);

                apiClient.Dispose();

                Assert.Pass();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
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
            try
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

                // wait for the cache to clear and the server to update the UpdateAt value, we don't just clear the cache because we want to make sure they are expiring
                await Task.Delay(300000);

                categoriesResponse = await Helpers.GetApiClient().Coins.Categories.GetCoinCategoriesAsync();

                Assert.IsNotNull(categoriesResponse);

                Assert.That(categoriesResponse, Is.Not.Empty);

                Assert.That(categoriesResponse.First().UpdatedAt, Is.Not.Null);

                Assert.That(categoriesResponse.First().UpdatedAt.Value, Is.Not.EqualTo(updatedAt));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task CacheEnableDisableTest()
        {
            try
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
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task PingTest()
        {
            try
            {
                var pingResult = await Helpers.GetApiClient().PingAsync();

                Assert.That(pingResult, Is.True);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetExchangeRatesTest()
        {
            try
            {
                var ratesResult = await Helpers.GetApiClient().GetExchangeRatesAsync();

                Assert.That(ratesResult, Is.Not.Null);
                Assert.That(ratesResult.Rates, Is.Not.Null);
                Assert.That(ratesResult.Rates, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetAssetPlatformsTest()
        {
            try
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
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
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
