using System;
using CountingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPeoplePage : ContentPage
    {
        private readonly SelectPeoplePageViewModel _viewModel;

        public SelectPeoplePage()
        {
            InitializeComponent();
        }

        public SelectPeoplePage(SelectPeoplePageViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(_viewModel, SelectPeoplePageViewModel.ApplyMessage);
            await Navigation.PopAsync(true);
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