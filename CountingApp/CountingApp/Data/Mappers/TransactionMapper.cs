using CountingApp.Data.Dto;
using CountingApp.Models;
using CountingApp.Models.Transactions;

namespace CountingApp.Data.Mappers
{
    static class TransactionMapper
    {
        public static TransactionDto Map(this Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                Timestamp = transaction.Timestamp,
                InnerObject = transaction
            };
        }

        public static Transaction Unmap(this TransactionDto dto)
        {
            // TODO: вохможность мапить любой тип транзакций
            var t = dto.InnerObject as Purchase;
            if (t == null)
                return null;
            return new Purchase()
            {
                Id = dto.Id,
                Timestamp = dto.Timestamp,
                Contributions = t.Contributions,
                People = t.People
            };
        }
    }
}
