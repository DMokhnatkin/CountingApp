using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CountingApp.Core.Dto;
using System.Linq;

namespace CountingApp.Data.Repositories.Transactions
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private List<TransactionDto> _transactions = new List<TransactionDto>() {
            new TransactionDto(){ Id = Guid.NewGuid(), Timestamp = DateTime.Now, TotalAmount = 999 }
        };

        public async Task<TransactionDto> AddAsync(TransactionDto dto)
        {
            await Task.Delay(50);
            _transactions.Add(dto);
            return dto;
        }

        public async Task<TransactionDto[]> GetAllAsync()
        {
            await Task.Delay(50);
            return _transactions.ToArray();
        }

        public async Task<TransactionDto> GetAsync(Guid id)
        {
            await Task.Delay(50);

            return _transactions.FirstOrDefault(t => t.Id == id);
        }

        public async Task ModifyAsync(TransactionDto dto)
        {
            await Task.Delay(50);
            // 
            var index = _transactions.IndexOf(dto);
            if (index > 0)
                _transactions[index] = dto;
        }

        public async Task RemoveAsync(Guid id)
        {
            await Task.Delay(50);
            var dto = _transactions.FirstOrDefault(t => t.Id == id);
            _transactions.Remove(dto);
        }
    }
}
