using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using CountingApp.Core.Config;
using CountingApp.Core.Dto;
using CountingApp.Models;
using CountingApp.Models.Transactions;
using CountingApp.Services;
using Newtonsoft.Json;
using System.Text;

namespace CountingApp.Data.Repositories.Transactions
{
    public class HttpTransactionsRepository : ITransactionsRepository
    {
        private static string _baseAddress = Uris.CoreServerUri + "/api/transactions";

        private readonly HttpClient _client;

        public HttpTransactionsRepository()
        {
            _client = ApplicationIocContainer.CurrentContainer.Resolve<IAuthService>().GetClient();
        }

        public async Task<TransactionDto[]> GetAllAsync()
        {
            var res = await _client.GetAsync(_baseAddress);
            res.EnsureSuccessStatusCode();
            var resJson = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TransactionDto[]>(resJson);
        }

        public async Task<TransactionDto> GetAsync(Guid id)
        {
            var res = await _client.GetAsync(_baseAddress + $"/{id.ToString()}");
            res.EnsureSuccessStatusCode();
            var resJson = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TransactionDto>(resJson);
        }

        public async Task AddAsync(TransactionDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync($"{_baseAddress}", content);
            result.EnsureSuccessStatusCode();
        }

        public async Task ModifyAsync(TransactionDto dto)
        {
            var res = await _client.PutAsync(_baseAddress, new StringContent(JsonConvert.SerializeObject(dto)));
            res.EnsureSuccessStatusCode();
        }

        public async Task RemoveAsync(Guid id)
        {
            var res = await _client.DeleteAsync(_baseAddress + $"/{id.ToString()}");
            res.EnsureSuccessStatusCode();
        }
    }
}

