using System;

namespace CountingApp.Models
{
    public class Debt
    {
        public Guid ContributorId { get; set; }

        public Guid FreeloaderId { get; set; }

        public decimal AmountRub { get; set; }

        public Debt(Guid contributorId, Guid freeloaderId, decimal amountRub)
        {
            ContributorId = contributorId;
            FreeloaderId = freeloaderId;
            AmountRub = amountRub;
        }
    }
}
