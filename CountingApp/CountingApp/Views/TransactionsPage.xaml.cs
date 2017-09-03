using System;
using CountingApp.ViewModels;
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
            await Navigation.PushAsync(new PurchasePage());

            MessagingCenter.Subscribe<PurchasePage>(this, PurchasePage.DoneMessage, page =>
            {
                _viewModel.Transactions.Add(page.ViewModel.GetModel());

                MessagingCenter.Unsubscribe<PurchasePage>(this, PurchasePage.DoneMessage);
            });
        }
    }
}