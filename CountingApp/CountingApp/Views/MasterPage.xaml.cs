using System;
using System.Collections.Generic;
using System.Net.Http;
using Autofac;
using CountingApp.Services;
using CountingApp.ViewModels;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
	    public ListView ListView => listView;

	    public string CurUserDisplayName { get; set; }

	    public MasterPage ()
		{
			InitializeComponent ();

            CurUserDisplayName = "test";

		    var masterPageItems = new List<MasterPageItem>
		    {
		        new MasterPageItem
		        {
		            Title = "Транзакции",
		            IconSource = "ic_account_balance_wallet_black_24dp.png",
		            TargetType = typeof(TransactionsPage),
		            TargetTypeViewModel = typeof(TransactionsViewModel)
		        },
		        new MasterPageItem
		        {
		            Title = "Долги",
		            IconSource = "ic_compare_arrows_black_24dp",
		            TargetType = typeof(DebtsPage),
		            TargetTypeViewModel = typeof(DebtsViewModel)
		        }
		    };
		    listView.ItemsSource = masterPageItems;

		    listView.SelectedItem = masterPageItems[0];
		}

	    public class MasterPageItem
	    {
	        public string Title { get; set; }

	        public string IconSource { get; set; }

	        public Type TargetType { get; set; }

            public Type TargetTypeViewModel { get; set; }
        }

    }
}