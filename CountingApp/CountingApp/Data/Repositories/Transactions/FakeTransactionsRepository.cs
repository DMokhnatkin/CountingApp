using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CountingApp.Core.Dto;

namespace CountingApp.Data.Repositories.Transactions
{
    public class FakeTransactionsRepository : ITransactionsRepository
    {
        private readonly List<TransactionDto> _dtos = new List<TransactionDto>();

        public async Task<TransactionDto[]> GetAllAsync()
        {
            await Task.Delay(10);
            return _dtos.ToArray();
        }

        public async Task AddAsync(TransactionDto dto)
        {
            await Task.Delay(10);
            _dtos.Add(dto);
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

        public async Task ModifyAsync(TransactionDto dto)
        {
            await Task.Delay(10);
            var index = FindById(dto.Id);
            if (index == -1)
                throw new IndexOutOfRangeException();
            _dtos[index] = dto;
        }

        public async Task RemoveAsync(Guid id)
        {
            await Task.Delay(10);
            var index = FindById(id);
            if (index == -1)
                throw new IndexOutOfRangeException();
            _dtos.RemoveAt(index);
        }
    }
}
