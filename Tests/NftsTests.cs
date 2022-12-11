namespace Tests
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

        [Test]
        public async Task GetNftTest()
        {
            await Helpers.DoRateLimiting();

            var nftsResult = await _apiClient.Nfts.GetNftAsync("8bit");

            Assert.That(nftsResult, Is.Not.Null);
            Assert.That(nftsResult.Name, Is.EqualTo("8 Bit Universe"));

            await Helpers.DoRateLimiting();

            nftsResult = await _apiClient.Nfts.GetNftAsync("ethereum", "0xaae71bbbaa359be0d81d5cbc9b1e88a8b7c58a94");

            Assert.That(nftsResult, Is.Not.Null);
            Assert.That(nftsResult.Name, Is.EqualTo("8 Bit Universe"));
        }
    }
}
