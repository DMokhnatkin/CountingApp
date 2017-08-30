using System;

namespace CountingApp.Models
{
    public class Contribution
    {
        public Guid PersonId { get; set; }

        public decimal Amount { get; set; }
    }
}
