﻿namespace Tests
{
    public class NftsTests
    {
        private CoinGeckoClient _apiClient;

        [SetUp]
        public void Setup()
        {
            _apiClient = new CoinGeckoClient();
        }

        [Test]
        public async Task GetNftsListTest()
        {
            await Helpers.DoRateLimiting();

            var nftsResult = await _apiClient.Nfts.GetNftsListAsync();

            Assert.That(nftsResult, Is.Not.Null);
            Assert.That(nftsResult, Is.Not.Empty);
        }
    }
}
