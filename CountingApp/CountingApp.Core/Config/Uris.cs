namespace CountingApp.Core.Config
{
    public class Uris
    {
        public const string IdentityServerHost = "pc.mokhnatkin.org";
        public const string IdentityServerPort = "5050";
        public const string IdentityServerUri = "http://" + IdentityServerHost + ":" + IdentityServerPort;

        public const string CoreServerHost = "pc.mokhnatkin.org";
        public const string CoreServerPort = "5051";
        public const string CoreServerUri = "http://" + CoreServerHost + ":" + CoreServerPort;
    }
}
