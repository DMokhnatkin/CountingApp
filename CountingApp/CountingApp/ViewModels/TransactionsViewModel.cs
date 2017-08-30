using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CountingApp.Models;
using CountingApp.Models.Transactions;
using CountingApp.Views;
using CountingApp.Views.Transactions;
using Xamarin.Forms;

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

        public ICommand CreateTransactionCommand { get; set; }

        private INavigation _navigation;

        public TransactionsViewModel(INavigation navigation)
        {
            _navigation = navigation;

            CreateTransactionCommand = new Command(AddTransactionExecute);

            Transactions = new ObservableCollection<Transaction>();
        }

        private async void AddTransactionExecute()
        {
            await _navigation.PushModalAsync(new CreateTransactionPage());
        }
    }
}
