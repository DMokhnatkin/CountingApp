using System;
using System.Threading.Tasks;
using CountingApp.Core.Dto;

namespace CountingApp.Data.Repositories.Transactions
{
    public interface ITransactionsRepository
    {
        Task<TransactionDto[]> GetAllAsync();

        Task<TransactionDto> Get(Guid id);

        Task AddAsync(TransactionDto dto);

        Task ModifyAsync(TransactionDto dto);

        Task RemoveAsync(Guid id);
    }
}
