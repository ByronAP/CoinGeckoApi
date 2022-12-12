namespace Tests
{
    public class DerivativesTests
    {
        [Test]
        public async Task GetDerivativesTest()
        {
            await Helpers.DoRateLimiting();

            var derivativesResult = await Helpers.GetApiClient().Derivatives.GetDerivativesAsync();

            Assert.That(derivativesResult, Is.Not.Null);
            Assert.That(derivativesResult, Is.Not.Empty);
        }

        [Test]
        public async Task GetDerivativesExchangesTest()
        {
            await Helpers.DoRateLimiting();

            var derivativesResult = await Helpers.GetApiClient().Derivatives.GetDerivativesExchangesAsync();

            Assert.That(derivativesResult, Is.Not.Null);
            Assert.That(derivativesResult, Is.Not.Empty);
        }

        [Test]
        public async Task GetDerivativesExchangeTest()
        {
            await Helpers.DoRateLimiting();

            var derivativesResult = await Helpers.GetApiClient().Derivatives.GetDerivativesExchangeAsync("zbg_futures");

            Assert.That(derivativesResult, Is.Not.Null);
            Assert.That(derivativesResult.YearEstablished, Is.GreaterThan(2000));
        }

        [Test]
        public async Task GetDerivativesExchangesListTest()
        {
            await Helpers.DoRateLimiting();

            var derivativesResult = await Helpers.GetApiClient().Derivatives.GetDerivativesExchangesListAsync();

            Assert.That(derivativesResult, Is.Not.Null);
            Assert.That(derivativesResult, Is.Not.Empty);
        }
    }
}
