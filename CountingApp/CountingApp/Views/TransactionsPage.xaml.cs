using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CountingApp.Data.Mappers;
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

            BindingContext = _viewModel = new TransactionsViewModel();
        }

        private async void AddPurchase_OnClicked(object sender, EventArgs e)
        {
            MessagingCenter.Subscribe<PurchasePage>(this, PurchasePage.DoneMessage, async page =>
            {
                MessagingCenter.Unsubscribe<PurchasePage>(this, PurchasePage.DoneMessage);

                var model = page.ViewModel.GetModel();
                IsBusy = true;
                try
                {
                    var dto = model.Map();
                    var transactionsRep = DependencyService.Get<ITransactionsRepository>();
                    await transactionsRep.AddAsync(dto);
                    await _viewModel.ReloadTransaction(dto.Id);
                }
                catch (Exception exc)
                {
                    Debug.Fail(exc.Message);
                    // TODO: reopen page
                }
                IsBusy = false;
            });

            await Navigation.PushAsync(new PurchasePage());
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            if (((MenuItem)sender).CommandParameter is TransactionListItemViewModel transactionListItemViewModel)
            {
                await _viewModel.RemoveTransaction(transactionListItemViewModel.Model.Id);
            }
        }

        private async void OnEdit(object sender, EventArgs e)
        {
            if (((MenuItem)sender).CommandParameter is TransactionListItemViewModel transactionListItemViewModel)
            {
                MessagingCenter.Subscribe<PurchasePage>(this, PurchasePage.DoneMessage, async page =>
                {
                    MessagingCenter.Unsubscribe<PurchasePage>(this, PurchasePage.DoneMessage);

                    var model = page.ViewModel.GetModel();
                    await _viewModel.ModifyTransaction(model);
                });

                // TODO: возможность работы с более чем одним типом транзакций
                await Navigation.PushAsync(new PurchasePage(new PurchaseViewModel(transactionListItemViewModel.Model as Purchase)));
            }
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            // TODO: remove
            // Нам не нужно выделение в ListView
            if (sender is ListView listView)
            {
                listView.SelectedItem = null;
            }
        }

        private async void TransactionsPage_OnAppearing(object sender, EventArgs e)
        {
            await _viewModel.ReloadTransactions();
        }
    }
}