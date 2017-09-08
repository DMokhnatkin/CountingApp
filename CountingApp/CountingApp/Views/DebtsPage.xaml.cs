using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountingApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DebtsPage : ContentPage
	{
	    private DebtsViewModel _viewModel;

		public DebtsPage ()
		{
			InitializeComponent ();
		    BindingContext = _viewModel = new DebtsViewModel();
#pragma warning disable 4014
		    _viewModel.CalculateDebts();
#pragma warning restore 4014
		}
	}
}