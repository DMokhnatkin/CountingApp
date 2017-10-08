namespace CountingApp.Helpers
{
    public static class Constants
    {
        public static string AppName = "CountingApp";

        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string AndroidClientId = "1025525644396-nbq117quojdkrn7il5ccqjqoit48ii07.apps.googleusercontent.com";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "http://192.168.1.141:5050/account/login";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public const string AndroidReversedClientId = "com.googleusercontent.apps.1025525644396-nbq117quojdkrn7il5ccqjqoit48ii07";
        public const string AndroidRedirectUrl = AndroidReversedClientId + ":/oauth2redirect";
    }
}
