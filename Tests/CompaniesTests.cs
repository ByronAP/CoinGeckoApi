namespace Tests
{
    public class CompaniesTests
    {
        [Test]
        public async Task GetCompaniesPublicTreasuryTest()
        {
            try
            {
                var companiesResult = await Helpers.GetApiClient().Companies.GetCompaniesPublicTreasuryAsync();

                Assert.That(companiesResult, Is.Not.Null);
                Assert.That(companiesResult.Companies, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }
    }
}
