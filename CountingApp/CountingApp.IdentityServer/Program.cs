using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using CountingApp.Core.Config;

namespace CountingApp.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "CountingApp.IdentityServer";

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(Uris.IdentityServerUri)
                .Build();
    }
}
