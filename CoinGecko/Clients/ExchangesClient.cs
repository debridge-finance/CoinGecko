using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinGecko.ApiEndPoints;
using CoinGecko.Entities.Response.Exchanges;
using CoinGecko.Interfaces;

using Newtonsoft.Json;

namespace CoinGecko.Clients
{
    public class ExchangesClient:BaseApiClient,IExchangesClient
    {
        public ExchangesClient(HttpClient httpClient, JsonSerializerSettings serializerSettings, string apiKey = null) : base(httpClient, serializerSettings, apiKey)
        {
        }
        
        public async Task<IReadOnlyList<Exchanges>> GetExchanges()
        {
            return await GetExchanges(100, "").ConfigureAwait(false);
        }
        public async Task<IReadOnlyList<Exchanges>> GetExchanges(int perPage,string page)
        {
            return await GetAsync<IReadOnlyList<Exchanges>>(
                AppendQueryString(ExchangesApiEndPoints.Exchanges,new Dictionary<string, object>
                {
                    {"per_page",perPage},
                    {"page",page}
                }
            )).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<ExchangesList>> GetExchangesList()
        {
            return await GetAsync<IReadOnlyList<ExchangesList>>(
                AppendQueryString(ExchangesApiEndPoints.ExchangesList)).ConfigureAwait(false);
        }

        public async Task<ExchangeById> GetExchangesByExchangeId(string id)
        {
            return await GetAsync<ExchangeById>(
                AppendQueryString(ExchangesApiEndPoints.ExchangeById(id) )).ConfigureAwait(false);
        }

        public async Task<TickerByExchangeId> GetTickerByExchangeId(string id)
        {
            return await GetTickerByExchangeId(id, new []{""}, null,"","").ConfigureAwait(false);
        }

        public async Task<TickerByExchangeId> GetTickerByExchangeId(string id,string page)
        {
            return await GetTickerByExchangeId(id, new []{""}, page,"","").ConfigureAwait(false);
        }

        public async Task<TickerByExchangeId> GetTickerByExchangeId(string id,string[] coinIds,string page,string includeExchangeLogo,string order)
        {
            return await GetAsync<TickerByExchangeId>(AppendQueryString(
                ExchangesApiEndPoints.TickerById(id), new Dictionary<string, object>
                {
                    {"page",page},
                    {"coin_ids",string.Join(",",coinIds)},
                    {"include_exchange_logo",includeExchangeLogo},
                    {"order",order}
                })).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<VolumeChart>> GetVolumeChartsByExchangeId(string id, int days)
        {
            return await GetAsync<IReadOnlyList<VolumeChart>>(AppendQueryString(
                ExchangesApiEndPoints.VolumeChartById(id), new Dictionary<string, object>
                {
                    {"days", days}
                })).ConfigureAwait(false);
        }
    }
}