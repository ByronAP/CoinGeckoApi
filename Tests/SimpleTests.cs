namespace Tests
{
    public class SimpleTests
    {
        [Test]
        public async Task GetPriceTest()
        {
            try
            {
                var ids = new[] { "bitcoin", "ethereum" };
                var vsCurrencies = new[] { "btc", "usd" };

                var priceResult = await Helpers.GetApiClient().Simple.GetPriceAsync(ids, vsCurrencies);

                Assert.NotNull(priceResult);
                foreach (var id in ids)
                {
                    Assert.That(priceResult.ContainsKey(id), Is.True);
                    Assert.That(priceResult[id].Count(), Is.EqualTo(2));
                }

                priceResult = await Helpers.GetApiClient().Simple.GetPriceAsync(ids, vsCurrencies, true);

                Assert.That(priceResult, Is.Not.Null);
                foreach (var id in ids)
                {
                    Assert.That(priceResult.ContainsKey(id), Is.True);
                    Assert.That(priceResult[id].Count(), Is.EqualTo(4));
                }

                priceResult = await Helpers.GetApiClient().Simple.GetPriceAsync(ids, vsCurrencies, true, true);

                Assert.NotNull(priceResult);
                foreach (var id in ids)
                {
                    Assert.That(priceResult.ContainsKey(id), Is.True);
                    Assert.That(priceResult[id].Count(), Is.EqualTo(6));
                }

                priceResult = await Helpers.GetApiClient().Simple.GetPriceAsync(ids, vsCurrencies, true, true, true);

                Assert.NotNull(priceResult);
                foreach (var id in ids)
                {
                    Assert.That(priceResult.ContainsKey(id), Is.True);
                    Assert.That(priceResult[id].Count(), Is.EqualTo(8));
                }

                priceResult = await Helpers.GetApiClient().Simple.GetPriceAsync(ids, vsCurrencies, true, true, true, true);

                Assert.NotNull(priceResult);
                foreach (var id in ids)
                {
                    Assert.That(priceResult.ContainsKey(id), Is.True);
                    Assert.That(priceResult[id].Count(), Is.EqualTo(9));
                }

                // precision can't be reliably tested because prices fluctuate causing less precision to be used despite the requested precision (EX: 0.123 is a valid response despite the precision requested being 4)
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetTokenPriceTest()
        {
            try
            {
                var contractAddresses = new[] { "0x514910771af9ca656af840dff83e8264ecf986ca", "0x0f2d719407fdbeff09d87557abb7232601fd9f29" };
                var vsCurrencies = new[] { "btc", "usd" };

                var priceResult = await Helpers.GetApiClient().Simple.GetTokenPriceAsync("ethereum", contractAddresses, vsCurrencies);

                Assert.That(priceResult, Is.Not.Null);
                foreach (var contractAddress in contractAddresses)
                {
                    Assert.That(priceResult.ContainsKey(contractAddress), Is.True);
                    Assert.That(priceResult[contractAddress].Count(), Is.EqualTo(2));
                }

                priceResult = await Helpers.GetApiClient().Simple.GetTokenPriceAsync("ethereum", contractAddresses, vsCurrencies, true);

                Assert.That(priceResult, Is.Not.Null);
                foreach (var contractAddress in contractAddresses)
                {
                    Assert.That(priceResult.ContainsKey(contractAddress), Is.True);
                    Assert.That(priceResult[contractAddress].Count(), Is.EqualTo(4));
                }

                priceResult = await Helpers.GetApiClient().Simple.GetTokenPriceAsync("ethereum", contractAddresses, vsCurrencies, true, true);

                Assert.That(priceResult, Is.Not.Null);
                foreach (var contractAddress in contractAddresses)
                {
                    Assert.That(priceResult.ContainsKey(contractAddress), Is.True);
                    Assert.That(priceResult[contractAddress].Count(), Is.EqualTo(6));
                }

                priceResult = await Helpers.GetApiClient().Simple.GetTokenPriceAsync("ethereum", contractAddresses, vsCurrencies, true, true, true);

                Assert.That(priceResult, Is.Not.Null);
                foreach (var contractAddress in contractAddresses)
                {
                    Assert.That(priceResult.ContainsKey(contractAddress), Is.True);
                    Assert.That(priceResult[contractAddress].Count(), Is.EqualTo(8));
                }

                priceResult = await Helpers.GetApiClient().Simple.GetTokenPriceAsync("ethereum", contractAddresses, vsCurrencies, true, true, true, true);

                Assert.That(priceResult, Is.Not.Null);
                foreach (var contractAddress in contractAddresses)
                {
                    Assert.That(priceResult.ContainsKey(contractAddress), Is.True);
                    Assert.That(priceResult[contractAddress].Count(), Is.EqualTo(9));
                }

                // precision can't be reliably tested because prices fluctuate causing less precision to be used despite the requested precision (EX: 0.123 is a valid response despite the precision requested being 4)
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }

        [Test]
        public async Task GetSupportedVSCurrenciesTest()
        {
            try
            {
                var currsResult = await Helpers.GetApiClient().Simple.GetSupportedVSCurrenciesAsync();

                Assert.That(currsResult, Is.Not.Null);
                Assert.That(currsResult, Is.Not.Empty);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Assert.Warn(ex.Message);
            }
        }
    }
}