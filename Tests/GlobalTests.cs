namespace Tests
{
    public class GlobalTests
    {
        [Test]
        public async Task GetGlobalTest()
        {
            var globalResult = await Helpers.GetApiClient().Global.GetGlobalAsync();

            Assert.That(globalResult, Is.Not.Null);
            Assert.That(globalResult.Data, Is.Not.Null);
            Assert.That(globalResult.Data.ActiveCryptocurrencies, Is.GreaterThan(6000));
        }

        [Test]
        public async Task GetGlobalDefiTest()
        {
            var globalResult = await Helpers.GetApiClient().Global.GetGlobalDefiAsync();

            Assert.That(globalResult, Is.Not.Null);
            Assert.That(globalResult.Data, Is.Not.Null);
            Assert.That(Convert.ToDecimal(globalResult.Data.DefiDominance), Is.GreaterThan(1.1m));
        }
    }
}
