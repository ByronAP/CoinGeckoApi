namespace Tests
{
    internal class DITests
    {
        [Test]
        public void CreateViaDITest()
        {
            var services = new ServiceCollection();

            services.AddLogging();
            services.AddCoinGeckoApi();

            using var provider = services.BuildServiceProvider();

            var api = provider.GetService<CoinGeckoClient>();

            Assert.That(api, Is.Not.Null);

            Assert.That(api.CGRestClient.Options.BaseUrl, Is.Not.Null);
            Assert.That(api.CGRestClient.Options.BaseUrl.ToString(), Is.EqualTo(Constants.API_BASE_URL + "/"));
        }

        [Test]
        public void CreateViaDIWithApiKeyTest()
        {
            var services = new ServiceCollection();

            services.AddLogging();
            services.AddCoinGeckoApi("FakeApiKey");

            using var provider = services.BuildServiceProvider();

            var api = provider.GetService<CoinGeckoClient>();

            Assert.That(api, Is.Not.Null);

            Assert.That(api.CGRestClient.Options.BaseUrl, Is.Not.Null);
            Assert.That(api.CGRestClient.Options.BaseUrl.ToString(), Is.EqualTo(Constants.API_PRO_BASE_URL + "/"));
        }
    }
}
