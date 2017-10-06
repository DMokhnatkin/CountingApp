using System;
using CountingApp.Views;
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
