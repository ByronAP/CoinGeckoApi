namespace Tests
{
    public class CompaniesTests
    {
        [Test]
        public async Task GetCompaniesPublicTreasuryTest()
        {
            await Helpers.DoRateLimiting();

            var companiesResult = await Helpers.GetApiClient().Companies.GetCompaniesPublicTreasuryAsync();

            Assert.That(companiesResult, Is.Not.Null);
            Assert.That(companiesResult.Companies, Is.Not.Empty);
        }
    }
}
