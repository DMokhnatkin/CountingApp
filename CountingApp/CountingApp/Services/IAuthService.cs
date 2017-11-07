using System.Net.Http;
using RestSharp;
using Xamarin.Auth;

namespace CountingApp.Services
{
    public interface IAuthService
    {
        Account CurAccount { get; }

        bool IsAuthenticated { get; }

        string AuthToken { get; }

        WebAuthenticator CurAuthenticator { get; }

        void Login();

        HttpClient GetClient();

        RestClient GetRestClient();
    }
}
