using CountingApp.Core.Config;

namespace CountingApp.Helpers
{
    public static class Constants
    {
        public const string AppName = "CountingApp";
        //For return back in application we will require redirect to reversed IdentityServerHost (e.g. api1.google.com -> com.google.api1)
        // TODO: remove hardcode
        public const string IdentityServerHostReversed = "org.mokhnatkin.pc";

        // These values do not need changing
        public static string AuthorizeUrl = $"{Uris.IdentityServerUri}/connect/authorize";
        public static string AccessTokenUrl = $"{Uris.IdentityServerUri}/connect/token";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string AndroidRedirectUrl = $"{IdentityServerHostReversed}:/oauth2redirect";
    }
}
