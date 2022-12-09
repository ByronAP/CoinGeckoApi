﻿using CoinGeckoAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    public class CoinsContractImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal CoinsContractImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// TODO: Document this.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="contract_address">The contract address.</param>
        /// <returns>A Task&lt;CoinContractResponse&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)</exception>
        /// <exception cref="System.ArgumentNullException">contract_address - Invalid value. Value must be a valid contract address (EX: 0x514910771af9ca656af840dff83e8264ecf986ca)</exception>
        public async Task<CoinContractResponse> GetCoinContractAsync(string id, string contract_address)
        {
            if (string.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid coin id (EX: bitcoin, ethereum)");
            }

            if (string.IsNullOrEmpty(contract_address) || String.IsNullOrWhiteSpace(contract_address))
            {
                throw new ArgumentNullException(nameof(contract_address), "Invalid value. Value must be a valid contract address (EX: 0x514910771af9ca656af840dff83e8264ecf986ca)");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("coins", id, "contract", contract_address));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<CoinContractResponse>(jsonStr);
        }
    }
}