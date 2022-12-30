namespace Tests
{
    public class GlobalTests
    {
        [Test]
        public async Task GetGlobalTest()
        {
            try
            {
                var globalResult = await Helpers.GetApiClient().Global.GetGlobalAsync();

                Assert.That(globalResult, Is.Not.Null);
                Assert.That(globalResult.Data, Is.Not.Null);
                Assert.That(globalResult.Data.ActiveCryptocurrencies, Is.GreaterThan(6000));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetGlobalDefiTest()
        {
            try
            {
                var globalResult = await Helpers.GetApiClient().Global.GetGlobalDefiAsync();

                Assert.That(globalResult, Is.Not.Null);
                Assert.That(globalResult.Data, Is.Not.Null);
                Assert.That(Convert.ToDecimal(globalResult.Data.DefiDominance), Is.GreaterThan(1.1m));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }
    }
}
