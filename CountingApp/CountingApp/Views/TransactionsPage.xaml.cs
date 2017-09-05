using System;
using CountingApp.Data.Repositories.Transactions;
using CountingApp.Models.Transactions;
using CountingApp.ViewModels;
using CountingApp.ViewModels.Transactions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        private readonly TransactionsViewModel _viewModel;

        public TransactionsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new TransactionsViewModel(new TransactionsRepository());
        }

        private async void AddPurchase_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PurchasePage());

            MessagingCenter.Subscribe<PurchasePage>(this, PurchasePage.DoneMessage, page =>
            {
                var model = page.ViewModel.GetModel();
                _viewModel.CreateTransaction(model);

                MessagingCenter.Unsubscribe<PurchasePage>(this, PurchasePage.DoneMessage);
            });
        }

        private async void TransactionsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is TransactionListItemViewModel transactionListItemViewModel)
            {
                // TODO: возможность работы с более чем одним типом транзакций
                await Navigation.PushAsync(new PurchasePage(new PurchaseViewModel(transactionListItemViewModel.Model as Purchase)));

                MessagingCenter.Subscribe<PurchasePage>(this, PurchasePage.DoneMessage, page =>
                {
                    var model = page.ViewModel.GetModel();
                    _viewModel.ModifyTransaction(model);

                    MessagingCenter.Unsubscribe<PurchasePage>(this, PurchasePage.DoneMessage);
                });
            }
        }
    }
}