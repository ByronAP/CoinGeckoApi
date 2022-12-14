namespace Tests
{
    public class CoinsContractTests
    {

        [Test]
        public async Task GetCoinContractTest()
        {
            try
            {
                var contractResult = await Helpers.GetApiClient().Coins.Contract.GetCoinContractAsync("ethereum", "0x514910771af9ca656af840dff83e8264ecf986ca");

                Assert.That(contractResult, Is.Not.Null);
                Assert.That(contractResult.Id, Is.EqualTo("chainlink"));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinContractMarketChartTest()
        {
            try
            {
                var chartResult = await Helpers.GetApiClient().Coins.Contract.GetCoinContractMarketChartAsync("ethereum", "0x514910771af9ca656af840dff83e8264ecf986ca", "usd", 30);

                Assert.That(chartResult, Is.Not.Null);
                Assert.That(chartResult.Prices, Is.Not.Empty);
                Assert.That(chartResult.MarketCaps, Is.Not.Empty);
                Assert.That(chartResult.TotalVolumes, Is.Not.Empty);

                var marketChartItems = chartResult.ToMarketChartCombinedItems();

                Assert.That(marketChartItems, Is.Not.Null);
                Assert.That(marketChartItems, Is.Not.Empty);
                Assert.That(marketChartItems.First().Price, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().MarketCap, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().TotalVolume, Is.GreaterThan(0));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetCoinMarketChartRangeTest()
        {
            try
            {
                var chartResult = await Helpers.GetApiClient().Coins.Contract.GetCoinContractMarketChartRangeAsync("ethereum", "0x514910771af9ca656af840dff83e8264ecf986ca", "usd", DateTimeOffset.UtcNow.AddMonths(-1), DateTimeOffset.UtcNow);

                Assert.That(chartResult, Is.Not.Null);
                Assert.That(chartResult.Prices, Is.Not.Empty);
                Assert.That(chartResult.MarketCaps, Is.Not.Empty);
                Assert.That(chartResult.TotalVolumes, Is.Not.Empty);

                var marketChartItems = chartResult.ToMarketChartCombinedItems();

                Assert.That(marketChartItems, Is.Not.Null);
                Assert.That(marketChartItems, Is.Not.Empty);
                Assert.That(marketChartItems.First().Price, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().MarketCap, Is.GreaterThan(0));
                Assert.That(marketChartItems.First().TotalVolume, Is.GreaterThan(0));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }
    }
}
