using System;

namespace CountingApp.Models.TransactionsAggregator
{
    public interface IDebtsAggregatorState : IAggregatorState
    {
        void IncreaseDebt(Guid who, Guid whom, decimal amountRub);

        void DecreaseDebt(Guid who, Guid whom, decimal amountRub);

        Debt[] GetAllDebts();
    }
}
