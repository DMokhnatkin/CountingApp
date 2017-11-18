using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using CountingApp.Data.Mappers;
using CountingApp.Data.Repositories.Transactions;
using CountingApp.Models.Transactions;
using CountingApp.Services;
using CountingApp.ViewModels;
using CountingApp.ViewModels.Transactions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CountingApp.Views.Transactions;

namespace CountingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionsPage : ContentPage
    {
        public TransactionsViewModel ViewModel => BindingContext as TransactionsViewModel;

        public TransactionsPage()
        {
            InitializeComponent();
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
                    var resDto = await transactionsRep.AddAsync(dto);
                    if (ViewModel != null) await ViewModel?.ReloadTransaction(resDto.Id);
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
                if (ViewModel != null) await ViewModel?.RemoveTransaction(transactionListItemViewModel.Model.Id);
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
                    if (ViewModel != null) await ViewModel?.ModifyTransaction(model);
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
    }
}