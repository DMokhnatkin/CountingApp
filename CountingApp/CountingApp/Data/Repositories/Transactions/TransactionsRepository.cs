using System.Collections.Generic;
using System.Threading.Tasks;
using CountingApp.Data.Dto;
using Xamarin.Forms;

namespace CountingApp.Data.Repositories.Transactions
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly List<TransactionDto> _dtos = new List<TransactionDto>();

        public async Task<TransactionDto[]> GetAllAsync()
        {
            await Task.Delay(200);
            return _dtos.ToArray();
        }

        public async Task<bool> Add(TransactionDto dto)
        {
            await Task.Delay(200);
            _dtos.Add(dto);
            return true;
        }

        private int FindById(TransactionDto dto)
        {
            for (int i = 0; i < _dtos.Count; i++)
            {
                if (_dtos[i].Id == dto.Id)
                    return i;
            }
            return -1;
        }

        public async Task<bool> Modify(TransactionDto dto)
        {
            await Task.Delay(200);
            var index = FindById(dto);
            if (index == -1)
                return false;
            _dtos[index] = dto;
            return true;
        }
    }
}
