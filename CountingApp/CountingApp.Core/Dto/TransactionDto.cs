using System;

namespace CountingApp.Core.Dto
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public string TransactionType { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal TotalAmount { get; set; }

        public string TransactionData { get; set; }
    }
}
