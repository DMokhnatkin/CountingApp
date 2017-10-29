using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using CountingApp.Core.Config;
using CountingApp.Data.Dto;
using CountingApp.Models;
using CountingApp.Models.Transactions;
using CountingApp.Services;
using Newtonsoft.Json;

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
            var r = await res.Content.ReadAsStringAsync();
            return new TransactionDto[0];
            //throw new NotImplementedException();
        }

        public Task<bool> Add(TransactionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Modify(TransactionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

