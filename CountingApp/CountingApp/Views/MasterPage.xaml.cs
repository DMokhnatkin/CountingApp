using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
	    public ListView ListView => listView;

	    public MasterPage ()
		{
			InitializeComponent ();

		    var masterPageItems = new List<MasterPageItem>();
		    masterPageItems.Add(new MasterPageItem
		    {
		        Title = "Transactions",
		        IconSource = "transactions.png",
		        TargetType = typeof(TransactionsPage)
		    });

		    listView.ItemsSource = masterPageItems;
        }

	    public class MasterPageItem
	    {
	        public string Title { get; set; }

	        public string IconSource { get; set; }

	        public Type TargetType { get; set; }
        }

    }
}