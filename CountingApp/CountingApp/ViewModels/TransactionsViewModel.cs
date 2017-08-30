using System.Collections.ObjectModel;
using CountingApp.Models;

namespace CountingApp.ViewModels
{
    public class TransactionsViewModel : BaseViewModel
    {
        private ObservableCollection<PurchaseItemViewModel> _transactions;
        public ObservableCollection<PurchaseItemViewModel> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }

        public TransactionsViewModel()
        {
            Transactions = new ObservableCollection<PurchaseItemViewModel>(new []
            {
                new PurchaseItemViewModel(new Purchase{ Contributions = new []
                {
                    new Contribution { Amount = 1000, PersonId = new Person { DisplayName = "ДМ" }},
                    new Contribution { Amount = 200, PersonId = new Person { DisplayName = "АМ" }}
                }}),
                new PurchaseItemViewModel(new Purchase{ Contributions = new []
                {
                    new Contribution { Amount = 1000, PersonId = new Person { DisplayName = "ДМ" }},
                    new Contribution { Amount = 1000, PersonId = new Person { DisplayName = "АМ" }}
                }}), 
            });
        }
    }
}
