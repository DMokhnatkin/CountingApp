using System;
using System.Diagnostics;
using System.Linq;
using Autofac;
using CountingApp.Helpers;
using Xamarin.Auth;
using Xamarin.Forms;

namespace CountingApp.Services
{
    public class AuthService : IAuthService
    {
        private Account _account;
        private readonly AccountStore _accountStore;

        public AuthService()
        {
            _accountStore = ApplicationIocContainer.CurrentContainer.Resolve<AccountStore>();
            _account = _accountStore.FindAccountsForService(Constants.AppName).FirstOrDefault();
        }

        public bool IsAuthenticated { get; private set; }

        public string AuthToken { get; private set; }

        public WebAuthenticator CurAuthenticator { get; private set; }

        public Account CurAccount => _account;

        public void Login()
        {
            string clientId = null;
            string clientSecret = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    throw new NotImplementedException();
                    break;

                case Device.Android:
                    clientId = ConstantsSecret.AndroidClientId;
                    clientSecret = ConstantsSecret.AndroidClientSecret;
                    redirectUri = Constants.AndroidRedirectUrl;
                    break;
            }

            CurAuthenticator = new OAuth2Authenticator(
                clientId,
                clientSecret,
                "api1",
                new Uri(Constants.AuthorizeUrl),
                new Uri(redirectUri ?? throw new InvalidOperationException()),
                new Uri(Constants.AccessTokenUrl),
                null,
                true);

            CurAuthenticator.Completed += OnAuthCompleted;
            CurAuthenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = CurAuthenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(CurAuthenticator);
        }

        private async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            if (e.IsAuthenticated)
            {
                if (_account != null)
                {
                    _accountStore.Delete(_account, Constants.AppName);
                }

                await _accountStore.SaveAsync(_account = e.Account, Constants.AppName);

                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                //var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                //var response = await request.GetResponseAsync();
                //if (response != null)
                //{
                //    // Deserialize the data and _accountStore it in the _account _accountStore
                //    // The users email address will be used to identify data in SimpleDB
                //    var userJObject = JObject.Parse(await response.GetResponseTextAsync());
                //    string userId = userJObject["id"].Value<string>();
                //    string displayName = userJObject["name"].Value<string>();
                //}
            }
        }

        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            if (sender is OAuth2Authenticator authenticator)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }
    }
}
