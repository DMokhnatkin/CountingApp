using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CountingApp.Data.Dto;

namespace CountingApp.Data.Repositories.Transactions
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly List<TransactionDto> _dtos = new List<TransactionDto>();

        public async Task<TransactionDto[]> GetAllAsync()
        {
            return await Task.FromResult(_dtos.ToArray());
        }

        public async Task<bool> Add(TransactionDto dto)
        {
            await Task.Run(() => _dtos.Add(dto));
            return true;
        }

        private int FindById(Guid id)
        {
            for (int i = 0; i < _dtos.Count; i++)
            {
                if (_dtos[i].Id == id)
                    return i;
            }
            return -1;
        }

        public async Task<bool> Modify(TransactionDto dto)
        {
            var index = await Task.FromResult(FindById(dto.Id));
            if (index == -1)
                return false;
            _dtos[index] = dto;
            return true;
        }

        public async Task<bool> Remove(Guid id)
        {
            var index = await Task.FromResult(FindById(id));
            if (index == -1)
                return false;
            _dtos.RemoveAt(index);
            return true;
        }
    }
}
