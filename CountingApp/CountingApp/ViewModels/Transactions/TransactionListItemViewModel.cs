using System;
using CountingApp.Models;

namespace CountingApp.ViewModels.Transactions
{
    public class TransactionListItemViewModel : BaseViewModel
    {
        public Transaction Model { get; private set; }

        public void ChangeModel(Transaction newModel)
        {
            Model = newModel;

            OnPropertyChanged(string.Empty);
        }

        public TransactionListItemViewModel(Transaction model)
        {
            ChangeModel(model);
        }

        #region Observable

        public DateTime Timestamp => Model.Timestamp;

        public decimal TotalAmountRub => Model.TotalAmountRub;

        #endregion
    }
}
