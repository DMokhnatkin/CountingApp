using System;

namespace CountingApp.Data.Dto
{
    public class TransactionDto
    {
        public Guid Id { get; set; }

        public DateTime Timestamp { get; set; }

        public object InnerObject { get; set; }
    }
}
