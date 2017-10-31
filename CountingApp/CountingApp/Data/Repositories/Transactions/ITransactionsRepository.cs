using System;
using System.Threading.Tasks;
using CountingApp.Core.Dto;

namespace CountingApp.Data.Repositories.Transactions
{
    public interface ITransactionsRepository
    {
        Task<TransactionDto[]> GetAllAsync();

        Task<bool> AddAsync(TransactionDto dto);

        Task<bool> ModifyAsync(TransactionDto dto);

        Task<bool> RemoveAsync(Guid id);
    }
}
