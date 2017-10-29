using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
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

            ApplicationIocContainer.BuildIocContainer();

		    RegisterDependencies();

            MainPage = new MainPage();
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
            // TODO: move to ApplicationIocContainer
	        DependencyService.Register<IPeopleRepository, PeopleRepository>();
	        DependencyService.Register<ITransactionsRepository, HttpTransactionsRepository>();
            DependencyService.Register<DebtsCalculationService>();
        }
	}
}
