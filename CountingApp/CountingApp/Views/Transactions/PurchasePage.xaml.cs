﻿using System;
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
		    ViewModel.Load();

            BindingContext = ViewModel;
		}

	    private async void ChangeContributors_OnClicked(object sender, EventArgs e)
	    {
            var selectPeopleViewModel = new SelectPeoplePageViewModel();
#pragma warning disable 4014
	        selectPeopleViewModel
                .LoadPeopleListAsync()
                .ContinueWith(task => 
                    selectPeopleViewModel.SetSelected(ViewModel.Contributors.Select(x => x.Model).ToArray()));
#pragma warning restore 4014
            var selectPeoplePage = new SelectPeoplePage(selectPeopleViewModel);

	        MessagingCenter.Subscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage, page =>
	        {
                // Нужно смержить выбранных до этого момента и выбранных после людей
                var selectedBefore = new HashSet<Guid>(ViewModel.Contributors.Select(x => x.Model.Id));
	            var selectedAfter = new Dictionary<Guid, Person>(page.GetSelected().ToDictionary(x => x.Id, v => v));

	            var merged = ViewModel.Contributors.ToDictionary(x => x.Model.Id, v => v);

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

	            ViewModel.Contributors = new ObservableCollection<PurchaseViewModel.ContributionViewModel>(merged.Values.OrderBy(x => x.DisplayName));

                MessagingCenter.Unsubscribe<SelectPeoplePageViewModel>(this, SelectPeoplePageViewModel.ApplyMessage);
	        });
	        await Navigation.PushAsync(selectPeoplePage);
	    }

	    private void ChangeFreeloaders_OnClicked(object sender, EventArgs e)
	    {
	        
	    }

	    private async void MenuItemDone_OnClicked(object sender, EventArgs e)
	    {
            MessagingCenter.Send(this, DoneMessage);
	        await Navigation.PopAsync();
	    }
	}
}