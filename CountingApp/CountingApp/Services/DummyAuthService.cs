using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using RestSharp;
using Xamarin.Auth;

namespace CountingApp.Services
{
    public class DummyAuthService : IAuthService
    {
        public Account CurAccount => throw new NotImplementedException();

        public bool IsAuthenticated => true;

        public string AuthToken => throw new NotImplementedException();

        public WebAuthenticator CurAuthenticator => throw new NotImplementedException();

        public HttpClient GetClient()
        {
            throw new NotImplementedException();
        }

        public RestClient GetRestClient()
        {
            throw new NotImplementedException();
        }

        public void Login()
        {
            throw new NotImplementedException();
        }
    }
}
