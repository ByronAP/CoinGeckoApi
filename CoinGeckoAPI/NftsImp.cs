// ***********************************************************************
// Assembly         : CoinGeckoAPI
// Author           : ByronAP
// Created          : 12-10-2022
//
// Last Modified By : ByronAP
// Last Modified On : 12-11-2022
// ***********************************************************************
// <copyright file="NftsImp.cs" company="ByronAP">
//     Copyright © 2022 ByronAP, CoinGecko. All rights reserved.
// </copyright>
// ***********************************************************************
using CoinGeckoAPI.Models;
using CoinGeckoAPI.Types;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinGeckoAPI
{
    /// <summary>
    /// <para>Implementation of the '/nfts' API calls.</para>
    /// <para>Implementation classes do not have a public constructor
    /// and must be accessed through an instance of <see cref="CoinGeckoClient"/>.</para>
    /// </summary>
    public class NftsImp
    {
        private readonly RestClient _restClient;
        private readonly ILogger<CoinGeckoClient> _logger;

        internal NftsImp(RestClient restClient, ILogger<CoinGeckoClient> logger = null)
        {
            _logger = logger;
            _restClient = restClient;
        }

        /// <summary>
        /// Use this to obtain all the NFT ids in order to make API calls.
        /// </summary>
        /// <param name="order">The ordering of results (sort) <see cref="NftsListOrderBy"/>. Default: none.</param>
        /// <param name="asset_platform_id">The id of the platform issuing tokens (See <see cref="CoinGeckoClient.GetAssetPlatformsAsync"/> for list of options).</param>
        /// <param name="per_page">Total results per page. Default: 100.</param>
        /// <param name="page">Page through results.</param>
        /// <returns>A Task&lt;IEnumerable&lt;<see cref="NftListItem"/>&gt;&gt; representing the asynchronous operation.</returns>
        public async Task<IEnumerable<NftListItem>> GetNftsListAsync(NftsListOrderBy order = NftsListOrderBy.None, string asset_platform_id = "", uint per_page = 100, uint page = 1)
        {
            if (page == 0) { page = 1; }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("nfts", "list"));
            if (order != NftsListOrderBy.None) { request.AddQueryParameter("order", order.ToString()); }
            if (!string.IsNullOrEmpty(asset_platform_id) && !string.IsNullOrWhiteSpace(asset_platform_id)) { request.AddQueryParameter("asset_platform_id", asset_platform_id); }
            if (per_page != 100) { request.AddQueryParameter("per_page", per_page); }
            if (page != 1) { request.AddQueryParameter("page", page); }

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<NftListItem[]>(jsonStr);
        }

        /// <summary>
        /// Get current data (name, price_floor, volume_24h ...) for an NFT collection. native_currency (string) is only a representative of the currency.
        /// </summary>
        /// <param name="id">The id of the nft collection (can be obtained from <see cref="GetNftsListAsync"/>).</param>
        /// <returns>A Task&lt;<see cref="NftResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">id - Invalid value. Value must be a valid NFT collection id (EX: 8bit).</exception>
        public async Task<NftResponse> GetNftAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id), "Invalid value. Value must be a valid NFT collection id (EX: 8bit).");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("nfts", id));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<NftResponse>(jsonStr);
        }

        /// <summary>
        /// Get current data (name, price_floor, volume_24h ...) for an NFT collection.
        /// </summary>
        /// <param name="asset_platform_id">The id of the platform issuing tokens (See <see cref="CoinGeckoClient.GetAssetPlatformsAsync"/> for list of options, use filter=nft param).</param>
        /// <param name="contract_address">The contract_address of the nft collection (See <see cref="GetNftsListAsync"/> for list of nft collection with metadata).</param>
        /// <returns>A Task&lt;<see cref="NftResponse"/>&gt; representing the asynchronous operation.</returns>
        /// <exception cref="System.ArgumentNullException">asset_platform_id - Invalid value. Value must be a valid NFT issuing platform (EX: ethereum).</exception>
        /// <exception cref="System.ArgumentNullException">contract_address - Invalid value. Value must be a valid NFT contract address.</exception>
        public async Task<NftResponse> GetNftAsync(string asset_platform_id, string contract_address)
        {
            if (string.IsNullOrEmpty(asset_platform_id) || string.IsNullOrWhiteSpace(asset_platform_id))
            {
                throw new ArgumentNullException(nameof(asset_platform_id), "Invalid value. Value must be a valid NFT issuing platform (EX: ethereum).");
            }

            if (string.IsNullOrEmpty(contract_address) || string.IsNullOrWhiteSpace(contract_address))
            {
                throw new ArgumentNullException(nameof(contract_address), "Invalid value. Value must be a valid NFT contract address.");
            }

            var request = new RestRequest(CoinGeckoClient.BuildUrl("nfts", asset_platform_id, "contract", contract_address));

            var jsonStr = await CoinGeckoClient.GetStringResponseAsync(_restClient, request, _logger);

            return JsonConvert.DeserializeObject<NftResponse>(jsonStr);
        }
    }
}
