using System;
using System.Collections.Generic;
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

		    _viewModel = new PurchaseViewModel();
            _viewModel.Load();

            BindingContext = _viewModel;
		}

	    private async void ChangeContributors_OnClicked(object sender, EventArgs e)
	    {
            var selectPeopleViewModel = new SelectPeoplePageViewModel();
#pragma warning disable 4014
	        selectPeopleViewModel
                .LoadPeopleListAsync()
                .ContinueWith(task => 
                    selectPeopleViewModel.SetSelected(_viewModel.Contributors.ToArray()));
#pragma warning restore 4014
            var selectPeoplePage = new SelectPeoplePage(selectPeopleViewModel);

	        MessagingCenter.Subscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage, page =>
	        {
                // Нужно смержить выбранных до этого момента и выбранных после людей
                var selectedBefore = new HashSet<Guid>(_viewModel.Contributors.Select(x => x.Id));
	            var selectedAfter = new Dictionary<Guid, Person>(page.GetSelected().ToDictionary(x => x.Id, v => v));

	            var merged = _viewModel.Contributors.ToDictionary(x => x.Id, v => v);

                // Сначала удаляем тех людей которые стали unchecked
	            foreach (var toRemove in selectedBefore.Except(selectedAfter.Keys))
	            {
	                merged.Remove(toRemove);
	            }

                // Затем добавляем тех людей, которые стали выбранны
	            foreach (var toAdd in selectedAfter.Keys.Except(selectedBefore))
	            {
	                merged[toAdd] = selectedAfter[toAdd];
	            }

                _viewModel.Contributors = new ObservableCollection<Person>(merged.Values.OrderBy(x => x.DisplayName));

                MessagingCenter.Unsubscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage);
	        });
	        await Navigation.PushAsync(selectPeoplePage);
	    }
	}
}