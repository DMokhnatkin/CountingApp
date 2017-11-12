using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CountingApp.Core.Dto;

namespace CountingApp.Data.Repositories.Transactions
{
    public class TransactionsRepository : ITransactionsRepository
    {
        public async Task<TransactionDto> AddAsync(TransactionDto dto)
        {
            await Task.Delay(50);
            return dto;
        }

        public async Task<TransactionDto[]> GetAllAsync()
        {
            await Task.Delay(50);
            return new TransactionDto[0];
        }

        public async Task<TransactionDto> GetAsync(Guid id)
        {
            await Task.Delay(50);

            return new TransactionDto();
        }

        public async Task ModifyAsync(TransactionDto dto)
        {
            await Task.Delay(50);
            
        }

        public async Task RemoveAsync(Guid id)
        {
            await Task.Delay(50);
        }
    }
}
