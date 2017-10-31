using CountingApp.Core.Dto;

namespace CountingApp.Server.DbModels.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDbModel Unmap(this TransactionDto dto)
        {
            return new TransactionDbModel
            {
                TransactionId = dto.Id,
                Timestamp = dto.Timestamp,
                TotalAmount = dto.TotalAmount,
                TransactionData = dto.TransactionData,
                TransactionType = dto.TransactionType,
                UserId = dto.UserId
            };
        }

        public static TransactionDto Map(TransactionDbModel dbModel)
        {
            return new TransactionDto
            {
                Id = dbModel.TransactionId,
                Timestamp = dbModel.Timestamp,
                TransactionType = dbModel.TransactionType,
                TotalAmount = dbModel.TotalAmount,
                TransactionData = dbModel.TransactionData,
                UserId = dbModel.UserId
            };
        }
    }
}
