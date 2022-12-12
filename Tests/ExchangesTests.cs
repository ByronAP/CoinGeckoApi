﻿namespace Tests
{
    public class ExchangesTests
    {
        [Test]
        public async Task GetExchangesTest()
        {
            await Helpers.DoRateLimiting();

            var exchangesResult = await Helpers.GetApiClient().Exchanges.GetExchangesAsync();

            Assert.That(exchangesResult, Is.Not.Null);
            Assert.That(exchangesResult, Is.Not.Empty);
        }

        [Test]
        public async Task GetExchangesListTest()
        {
            await Helpers.DoRateLimiting();

            var exchangesResult = await Helpers.GetApiClient().Exchanges.GetExchangesListAsync();

            Assert.That(exchangesResult, Is.Not.Null);
            Assert.That(exchangesResult, Is.Not.Empty);

            var gdaxItem = exchangesResult.First(x => x.Id.Equals("bitstamp", StringComparison.InvariantCultureIgnoreCase));

            Assert.That(gdaxItem, Is.Not.Null);
            Assert.That(gdaxItem.Name, Is.EqualTo("Bitstamp"));
        }

        [Test]
        public async Task GetExchangeTest()
        {
            await Helpers.DoRateLimiting();

            var exchangeResult = await Helpers.GetApiClient().Exchanges.GetExchangeAsync("gdax");

            Assert.That(exchangeResult, Is.Not.Null);
            Assert.That(exchangeResult.Tickers, Is.Not.Empty);
            Assert.That(exchangeResult.Name, Is.EqualTo("Coinbase Exchange"));
        }

        [Test]
        public async Task GetExchangeTickersTest()
        {
            await Helpers.DoRateLimiting();

            var tickersResult = await Helpers.GetApiClient().Exchanges.GetExchangeTickersAsync("gdax", new[] { "bitcoin", "ethereum", "cosmos" }, true);

            Assert.That(tickersResult, Is.Not.Null);
            Assert.That(tickersResult.Tickers, Is.Not.Empty);
            Assert.That(tickersResult.Name, Is.EqualTo("Coinbase Exchange"));
        }

        [Test]
        public async Task GetExchangeVolumeChartFriendlyTest()
        {
            await Helpers.DoRateLimiting();

            var chartResult = await Helpers.GetApiClient().Exchanges.GetExchangeVolumeChartFriendlyAsync("gdax", 2);

            Assert.That(chartResult, Is.Not.Null);
            Assert.That(chartResult, Is.Not.Empty);
        }

        [Test]
        public async Task GetExchangeVolumeChartTest()
        {
            await Helpers.DoRateLimiting();

            var chartResult = await Helpers.GetApiClient().Exchanges.GetExchangeVolumeChartAsync("gdax", 2);

            Assert.That(chartResult, Is.Not.Null);
            Assert.That(chartResult, Is.Not.Empty);
        }
    }
}
