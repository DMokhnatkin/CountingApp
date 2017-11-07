using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Autofac;
using CountingApp.Helpers;
using RestSharp;
using RestSharp.Authenticators;
using Xamarin.Auth;
using Xamarin.Forms;
using OAuth2Authenticator = Xamarin.Auth.OAuth2Authenticator;

namespace CountingApp.Services
{
    public class AuthService : IAuthService
    {
        private Account _account;
        private readonly AccountStore _accountStore;
        private HttpClient _curClient;
        private RestClient _curRestClient;

        public AuthService()
        {
            _accountStore = ApplicationIocContainer.CurrentContainer.Resolve<AccountStore>();
            _account = _accountStore.FindAccountsForService(Constants.AppName).FirstOrDefault();
        }

        public bool IsAuthenticated { get; private set; }

        public string AuthToken { get; private set; }

        public WebAuthenticator CurAuthenticator { get; private set; }

        public Account CurAccount => _account;

        public HttpClient GetClient()
        {
            if (_curClient != null)
                return _curClient;
            _curClient = new HttpClient();
            _curClient.SetBearerToken(CurAccount.Properties["access_token"]);
            return _curClient;
        }

        public RestClient GetRestClient()
        {
            if (_curRestClient != null)
                return _curRestClient;
            _curRestClient = new RestClient
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(CurAccount.Properties["access_token"], "Bearer")
            };
            //_curRestClient.AddDefaultHeader("Authorization", "Bearer " + CurAccount.Properties["access_token"]);
            return _curRestClient;
        }

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
                "CoreApi",
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

                //var client = new HttpClient();
                //client.SetBearerToken(e.Account.Properties["access_token"]);
                //client.BaseAddress = new Uri("http://pc.mokhnatkin.org:5051/");
                //var res = await client.GetAsync(new Uri("/api/values/"));
                //{ }
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
