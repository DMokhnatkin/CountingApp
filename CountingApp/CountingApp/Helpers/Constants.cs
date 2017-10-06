namespace CountingApp.Helpers
{
    public static class Constants
    {
        public static string AppName = "CountingApp";

        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string AndroidClientId = "1025525644396-58pf9t4m1anrd8lbe8mu9simejjoid86.apps.googleusercontent.com";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string AndroidRedirectUrl = "localhost:/oauth2redirect";
    }
}
