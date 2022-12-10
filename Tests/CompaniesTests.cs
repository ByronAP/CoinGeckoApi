namespace Tests
{
    public class CompaniesTests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        [Test]
        public async Task GetCompaniesPublicTreasuryTest()
        {
            await Helpers.DoRateLimiting();

            var companiesResult = await _apiClient.Companies.GetCompaniesPublicTreasuryAsync();

            Assert.That(companiesResult, Is.Not.Null);
            Assert.That(companiesResult.Companies, Is.Not.Empty);
        }
    }
}
