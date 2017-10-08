using Xamarin.Auth;

namespace CountingApp.Services
{
    interface IAuthService
    {
        Account CurAccount { get; }

        bool IsAuthenticated { get; }

        string AuthToken { get; }

        WebAuthenticator CurAuthenticator { get; }

        void Login();
    }
}
