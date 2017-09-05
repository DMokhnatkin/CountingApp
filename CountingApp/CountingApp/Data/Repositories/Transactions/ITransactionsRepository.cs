using System.Threading.Tasks;
using CountingApp.Data.Dto;

namespace CountingApp.Data.Repositories.Transactions
{
    public interface ITransactionsRepository
    {
        Task<TransactionDto[]> GetAllAsync();

        Task<bool> Add(TransactionDto dto);

        Task<bool> Modify(TransactionDto dto);
    }
}
