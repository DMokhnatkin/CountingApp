using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountingApp.ViewModels.Transactions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreatePurchasePage : ContentPage
	{
		public CreatePurchasePage ()
		{
			InitializeComponent ();

            BindingContext = new CreatePurchaseViewModel();
		}
	}
}