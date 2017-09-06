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
	    public const string DoneMessage = "Done";

	    public PurchaseViewModel ViewModel { get; }

		public PurchasePage ()
		{
			InitializeComponent ();

		    ViewModel = new PurchaseViewModel();

            BindingContext = ViewModel;
		}

	    public PurchasePage(PurchaseViewModel viewModel)
	    {
	        InitializeComponent();

	        ViewModel = viewModel;

	        BindingContext = ViewModel;
	    }

        private async void ChangeContributors_OnClicked(object sender, EventArgs e)
	    {
            var selectPeopleViewModel = new SelectPeoplePageViewModel();
#pragma warning disable 4014
	        selectPeopleViewModel
                .LoadPeopleListAsync()
                .ContinueWith(task => 
                    selectPeopleViewModel.SetSelected(ViewModel.Contributions.Select(x => x.Model).ToArray()));
#pragma warning restore 4014
            var selectPeoplePage = new SelectPeoplePage(selectPeopleViewModel);

	        MessagingCenter.Subscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage, selectPeoplePageViewModel =>
	        {
                // Нужно смержить выбранных до этого момента и выбранных после людей
                var selectedBefore = new HashSet<Guid>(ViewModel.Contributions.Select(x => x.Model.Id));
	            var selectedAfter = new Dictionary<Guid, Person>(selectPeoplePageViewModel.GetSelected().ToDictionary(x => x.Id, v => v));

	            var merged = ViewModel.Contributions.ToDictionary(x => x.Model.Id, v => v);

                // Сначала удаляем тех людей которые стали unchecked
	            foreach (var toRemove in selectedBefore.Except(selectedAfter.Keys))
	            {
	                merged.Remove(toRemove);
	            }

                // Затем добавляем тех людей, которые стали выбранны
	            foreach (var toAdd in selectedAfter.Keys.Except(selectedBefore))
	            {
	                merged[toAdd] = new PurchaseViewModel.ContributionViewModel(selectedAfter[toAdd]) { Amount = 0 };
	            }

	            ViewModel.Contributions = new ObservableCollection<PurchaseViewModel.ContributionViewModel>(merged.Values.OrderBy(x => x.DisplayName));
                // Не забываем если человек был назначен в спонсоры, его нужно убрать их нахлебников
                ViewModel.Freeloaders = new ObservableCollection<Person>(ViewModel.Freeloaders.Where(x => !selectedAfter.ContainsKey(x.Id)));

                MessagingCenter.Unsubscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage);
	        });
	        await Navigation.PushAsync(selectPeoplePage);
	    }

	    private async void ChangeFreeloaders_OnClicked(object sender, EventArgs e)
	    {
	        var selectPeopleViewModel = new SelectPeoplePageViewModel();
#pragma warning disable 4014
	        selectPeopleViewModel
	            .LoadPeopleListAsync()
	            .ContinueWith(task =>
	            {
	                selectPeopleViewModel.SetSelected(ViewModel.Freeloaders.ToArray());
	                selectPeopleViewModel.SetUnActive(ViewModel.Contributions.Select(x => x.Model.Id).ToArray());
	            });
#pragma warning restore 4014
	        var selectPeoplePage = new SelectPeoplePage(selectPeopleViewModel);

	        MessagingCenter.Subscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage, selectPeoplePageViewModel =>
	        {
	            ViewModel.Freeloaders = new ObservableCollection<Person>(selectPeoplePageViewModel.GetSelected());

	            MessagingCenter.Unsubscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage);
	        });
	        await Navigation.PushAsync(selectPeoplePage);
        }

	    private async void MenuItemDone_OnClicked(object sender, EventArgs e)
	    {
            MessagingCenter.Send(this, DoneMessage);
	        await Navigation.PopAsync();
	    }
	}
}