using System;

namespace CountingApp.Models
{
    public class Debt
    {
        public string ContributorId { get; set; }

        public string FreeloaderId { get; set; }

        public decimal AmountRub { get; set; }

        public Debt(string contributorId, string freeloaderId, decimal amountRub)
        {
            ContributorId = contributorId;
            FreeloaderId = freeloaderId;
            AmountRub = amountRub;
        }
    }
}
