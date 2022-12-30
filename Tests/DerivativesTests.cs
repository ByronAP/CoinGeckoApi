namespace Tests
{
    public class DerivativesTests
    {
        [Test]
        public async Task GetDerivativesTest()
        {
            try
            {
                var derivativesResult = await Helpers.GetApiClient().Derivatives.GetDerivativesAsync();

                Assert.That(derivativesResult, Is.Not.Null);
                Assert.That(derivativesResult, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetDerivativesExchangesTest()
        {
            try
            {
                var derivativesResult = await Helpers.GetApiClient().Derivatives.GetDerivativesExchangesAsync();

                Assert.That(derivativesResult, Is.Not.Null);
                Assert.That(derivativesResult, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetDerivativesExchangeTest()
        {
            try
            {
                var derivativesResult = await Helpers.GetApiClient().Derivatives.GetDerivativesExchangeAsync("zbg_futures");

                Assert.That(derivativesResult, Is.Not.Null);
                Assert.That(derivativesResult.YearEstablished, Is.GreaterThan(2000));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetDerivativesExchangesListTest()
        {
            try
            {
                var derivativesResult = await Helpers.GetApiClient().Derivatives.GetDerivativesExchangesListAsync();

                Assert.That(derivativesResult, Is.Not.Null);
                Assert.That(derivativesResult, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }
    }
}
