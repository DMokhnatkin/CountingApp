﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
