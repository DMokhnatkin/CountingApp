using System;
using System.Diagnostics;
using System.Linq;
using CountingApp.Helpers;
using CountingApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CountingApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
	    Account _account;
	    AccountStore _store;

        public LoginPage ()
		{
			InitializeComponent ();

		    _store = AccountStore.Create();
		    _account = _store.FindAccountsForService(Constants.AppName).FirstOrDefault();
        }

        private void Login_OnClicked(object sender, EventArgs e)
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    throw new NotImplementedException();
                    break;

                case Device.Android:
                    clientId = Constants.AndroidClientId;
                    redirectUri = Constants.AndroidRedirectUrl;
                    break;
            }

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                "profile",
                new Uri(Constants.AuthorizeUrl),
                new Uri(redirectUri ?? throw new InvalidOperationException()),
                new Uri(Constants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            User user = null;
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and _store it in the _account _store
                    // The users email address will be used to identify data in SimpleDB
                    var userJObject = JObject.Parse(await response.GetResponseTextAsync());
                    string userId = userJObject["id"].Value<string>();
                    string displayName = userJObject["name"].Value<string>();
                }

                if (_account != null)
                {
                    _store.Delete(_account, Constants.AppName);
                }

                await _store.SaveAsync(_account = e.Account, Constants.AppName);
            }
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }
    }
}