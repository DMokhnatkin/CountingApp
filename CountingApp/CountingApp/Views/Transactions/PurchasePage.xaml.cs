using System;
using System.Collections.ObjectModel;
using System.Linq;
using CountingApp.Models;
using CountingApp.ViewModels;
using CountingApp.ViewModels.Transactions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PurchasePage : ContentPage
	{
	    private readonly PurchaseViewModel _viewModel;

		public PurchasePage ()
		{
			InitializeComponent ();

		    _viewModel = new PurchaseViewModel(Navigation);
            _viewModel.Load();

            BindingContext = _viewModel;
		}

	    private async void ChangeContributors_OnClicked(object sender, EventArgs e)
	    {
            var selectPeopleViewModel = new SelectPeoplePageViewModel();
#pragma warning disable 4014
	        selectPeopleViewModel.LoadPeopleListAsync();
#pragma warning restore 4014
            var selectPeoplePage = new SelectPeoplePage(selectPeopleViewModel);

	        MessagingCenter.Subscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage, page =>
	        {
	            var selected = page.GetSelected();
	            foreach (var person in selected)
	            {
	                if (!_viewModel.Contributors.Contains(person))
                        _viewModel.Contributors.Add(person);
	            }
                _viewModel.Contributors = new ObservableCollection<Person>(_viewModel.Contributors.OrderBy(x => x.DisplayName));

                MessagingCenter.Unsubscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage);
	        });
	        await Navigation.PushAsync(selectPeoplePage);
	    }
	}
}