using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CountingApp.Data.Repositories.People;
using CountingApp.Data.Repositories.Transactions;
using CountingApp.Services;
using CountingApp.Views;
using Xamarin.Forms;

namespace CountingApp
{
	public partial class App : Application
	{
		public App ()
		{
            InitializeComponent();

		    RegisterDependencies();

            MainPage = new LoginPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

	    private void RegisterDependencies()
	    {
	        DependencyService.Register<IPeopleRepository, PeopleRepository>();
	        DependencyService.Register<ITransactionsRepository, TransactionsRepository>();
            DependencyService.Register<DebtsCalculationService>();
        }
	}
}
