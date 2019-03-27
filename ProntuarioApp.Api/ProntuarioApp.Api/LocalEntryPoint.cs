using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ProntuarioApp.Api
{
    /// <summary>
    /// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
    /// </summary>
    public class LocalEntryPoint
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var host  = WebHost.CreateDefaultBuilder(args)
                    .UseLibuv()
                    .UseStartup<Startup>()
                    .Build();
            //This is a work around for Redis. For details take a look at the end of https://stackexchange.github.io/StackExchange.Redis/Timeouts
            int minWorker, minIOC;
            // Get the current settings.
            ThreadPool.GetMinThreads(out minWorker, out minIOC);

            // Change the minimum number of threads , using Environment.ProcessorCount and actual
            // setting as references
            minIOC = Math.Max(minIOC, Environment.ProcessorCount * 64);
            minWorker = Math.Max(minWorker, Environment.ProcessorCount * 64);
            ThreadPool.SetMinThreads(minWorker, minIOC);
            return host;
        }
    }
}
