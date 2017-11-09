using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CountingApp.Services;
using CountingApp.ViewModels;
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
		    if (masterPage.ListView.SelectedItem is MasterPage.MasterPageItem item)
		    {
		        LoadDetailPage(item);
		    }
		    else
		    {
		        throw new ArgumentException("Can't load first item from master page to detail");
		    }

	        if (Device.RuntimePlatform == Device.WinPhone)
	        {
	            MasterBehavior = MasterBehavior.Popover;
	        }
	    }

	    void LoadDetailPage(MasterPage.MasterPageItem item)
	    {
	        var page = (Page)Activator.CreateInstance(item.TargetType);
	        page.BindingContext = Activator.CreateInstance(item.TargetTypeViewModel);
	        if (page.BindingContext is ILoadableViewModel loadablevm)
	            // Await in different thread (so we wouldn't block GUI and will catch exceptions)
	            Task.Factory.StartNew(async () => await loadablevm.Load());

	        Detail = new NavigationPage(page);
	        masterPage.ListView.SelectedItem = null;
	        IsPresented = false;
        }

	    void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem is MasterPage.MasterPageItem item)
	        {
	            LoadDetailPage(item);
	        }
        }
	}
}
