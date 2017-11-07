using System;

namespace CountingApp.Models.TransactionsAggregator
{
    public interface IDebtsAggregatorState : IAggregatorState
    {
        void IncreaseDebt(string who, string whom, decimal amountRub);

        void DecreaseDebt(string who, string whom, decimal amountRub);

        Debt[] GetAllDebts();
    }
}
