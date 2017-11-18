using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using Autofac;
using CountingApp.Helpers;
using Xamarin.Auth;
using Xamarin.Forms;
using OAuth2Authenticator = Xamarin.Auth.OAuth2Authenticator;

namespace CountingApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly AccountStore _accountStore;

        public AuthService()
        {
            _accountStore = ApplicationIocContainer.CurrentContainer.Resolve<AccountStore>();
            CurAccount = _accountStore.FindAccountsForService(Constants.AppName).FirstOrDefault();
        }

        /// <summary>
        /// Whether a token for user was received and it is not expired.
        /// </summary>
        public bool IsAuthenticated => _curAccessToken != null && _curAccessToken.ValidTo > DateTime.UtcNow;

        public WebAuthenticator CurAuthenticator { get; private set; }

        // Just cache for CurAccount.Properties["access_token"]
        private JwtSecurityToken _curAccessToken;

        private Account _curAccount;
        public Account CurAccount
        {
            get => _curAccount;
            private set
            {
                _curAccount = value;
                if (value != null)
                    _curAccessToken = value.Properties.ContainsKey("access_token") ? new JwtSecurityToken(value.Properties["access_token"]) : null;
            }   
        }

        /// <summary>
        /// Returns HttpClient with setted authorization header (if user is not signed in, this method will call Sign In)
        /// </summary>
        public HttpClient GetClient()
        {
            if (IsAuthenticated)
                return BuildClient();

            SignIn();
            return BuildClient();
        }

        private HttpClient BuildClient()
        {
            var client = new HttpClient();
            client.SetBearerToken(_curAccessToken.RawData);
            return client;
        }

        /// <summary>
        /// Sign user in system. This method can show ui for loggin (if required)
        /// </summary>
        public void SignIn()
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
                if (CurAccount != null)
                {
                    _accountStore.Delete(CurAccount, Constants.AppName);
                }

                await _accountStore.SaveAsync(CurAccount = e.Account, Constants.AppName);
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
