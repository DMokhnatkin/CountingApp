﻿using System;
using Autofac;
using CountingApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public LoginPage ()
		{
			InitializeComponent ();
        }

        private void Login_OnClicked(object sender, EventArgs e)
        {
            var authService = ApplicationIocContainer.CurrentContainer.Resolve<IAuthService>();
            if (!authService.IsAuthenticated)
                authService.Login();
        }
    }
}