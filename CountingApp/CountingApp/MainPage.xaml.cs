using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CountingApp.ViewModels;
using CountingApp.Views;
using Xamarin.Forms;

namespace CountingApp
{
	public partial class MainPage : MasterDetailPage
	{
	    private readonly List<Page> _pages = new List<Page>();

		public MainPage()
		{
			InitializeComponent();

		    foreach (var item in masterPage.ListView.ItemsSource.OfType<MasterPage.MasterPageItem>())
		    {
		        InstantiatePage(item);
		    }

		    if (masterPage.ListView.SelectedItem is MasterPage.MasterPageItem firstPageItem)
		        ShowDetailPage(firstPageItem);
		    else
		        throw new ArgumentException("Can't load first item from master page to detail");

            masterPage.ListView.ItemSelected += OnItemSelected;

            if (Device.RuntimePlatform == Device.WinPhone)
	        {
	            MasterBehavior = MasterBehavior.Popover;
	        }
		}

        /// <summary>
        /// Create and add to _pages
        /// </summary>
        void InstantiatePage(MasterPage.MasterPageItem item)
	    {
            try
            {

                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.BindingContext = Activator.CreateInstance(item.TargetTypeViewModel);
                _pages.Add(page);
            } catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Load page data
        /// </summary>
	    async Task LoadPageData(Page page)
	    {
	        if (page.BindingContext is ILoadableViewModel loadablevm)
	        {
	            try
	            {
	                await loadablevm.Load();
                }
                catch (Exception e)
	            {
	                Debug.Fail(e.Message);
	            }
            }
        }

        /// <summary>
        /// This method will set up detail page and then start loading data for this page (async)
        /// </summary>
	    async void ShowDetailPage(MasterPage.MasterPageItem item)
	    {
	        var page = _pages.SingleOrDefault(x => x.Title == item.Title);
	        if (page == null)
	            throw new ArgumentException($"Can't find page with {item.Title} title");
            Detail = new NavigationPage(page);
	        IsPresented = false;

            await LoadPageData(page);
        }

	    void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        if (e.SelectedItem is MasterPage.MasterPageItem item)
	        {
	            ShowDetailPage(item);
	        }
	    }
	}
}
