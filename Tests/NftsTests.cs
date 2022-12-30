namespace Tests
{
    public class NftsTests
    {
        [Test]
        public async Task GetNftsListTest()
        {
            try
            {
                var nftsResult = await Helpers.GetApiClient().Nfts.GetNftsListAsync();

                Assert.That(nftsResult, Is.Not.Null);
                Assert.That(nftsResult, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetNftTest()
        {
            try
            {
                var nftsResult = await Helpers.GetApiClient().Nfts.GetNftAsync("8bit");

                Assert.That(nftsResult, Is.Not.Null);
                Assert.That(nftsResult.Name, Is.EqualTo("8 Bit Universe"));

                nftsResult = await Helpers.GetApiClient().Nfts.GetNftAsync("ethereum", "0xaae71bbbaa359be0d81d5cbc9b1e88a8b7c58a94");

                Assert.That(nftsResult, Is.Not.Null);
                Assert.That(nftsResult.Name, Is.EqualTo("8 Bit Universe"));
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }
    }
}
