using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CountingApp.Helpers;

namespace CountingApp.Droid
{
    [Activity(Label = "AuthInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataSchemes = new[] { Constants.IdentityServerHostReversed },
        DataPath = ":/oauth2redirect")]
    public class AuthInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Convert Android.Net.Url to Uri
            var uri = new Uri(Intent.Data.ToString());

            // Load redirectUrl page
            AuthenticationState.Authenticator?.OnPageLoading(uri);

            Finish();
        }
    }
}