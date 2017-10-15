namespace CountingApp.Helpers
{
    public static class Constants
    {
        public const string AppName = "CountingApp";
        public const string IdentityServerHost = "pc.mokhnatkin.org";
        public const string IdentityServerPort = "5050";
        // Used in redirect
        public const string IdentityServerHostReversed = "org.mokhnatkin.pc";

        // These values do not need changing
        public static string Scope = "api1";
        public static string AuthorizeUrl = $"http://{IdentityServerHost}:{IdentityServerPort}/connect/authorize";
        public static string AccessTokenUrl = $"http://{IdentityServerHost}:{IdentityServerPort}/connect/token";
        //public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string AndroidRedirectUrl = $"{IdentityServerHostReversed}:/oauth2redirect";
    }
}
