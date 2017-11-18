using System.Net.Http;
using RestSharp;
using Xamarin.Auth;

namespace CountingApp.Services
{
    public interface IAuthService
    {
        Account CurAccount { get; }

        bool IsAuthenticated { get; }

        WebAuthenticator CurAuthenticator { get; }

        void SignIn();

        HttpClient GetClient();
    }
}
