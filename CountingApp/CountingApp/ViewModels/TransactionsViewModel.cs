using System.Collections.ObjectModel;
using CountingApp.Models;

namespace CountingApp.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private ObservableCollection<Transaction> _transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }

        public void CreateTransaction(Transaction transaction)
        {
            
        }

        public TransactionsViewModel()
        {
            Transactions = new ObservableCollection<Transaction>();
        }
    }
}
