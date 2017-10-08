using System;
using System.Diagnostics;
using Autofac;
using CountingApp.Services;
using CountingApp.Views;
using Xamarin.Auth;
using Xamarin.Forms;

namespace CountingApp
{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
			InitializeComponent();

	        masterPage.ListView.ItemSelected += OnItemSelected;

	        if (Device.RuntimePlatform == Device.WinPhone)
	        {
	            MasterBehavior = MasterBehavior.Popover;
	        }
	    }

	    protected override async void OnAppearing()
	    {
	        var authService = ApplicationIocContainer.CurrentContainer.Resolve<IAuthService>();
	        if (!authService.IsAuthenticated)
                authService.Login();

            base.OnAppearing();
	    }

	    void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem is MasterPage.MasterPageItem item)
	        {
	            Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
	            masterPage.ListView.SelectedItem = null;
	            IsPresented = false;
	        }
        }
	}
}
