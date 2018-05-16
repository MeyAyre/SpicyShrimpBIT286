using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
//add next 2 after seeding done DbInitializer.cs in Data file 
//using Microsoft.Extensions.DependencyInjection;
//using BIT285_SpicyShrimps.Data;
namespace BIT285_SpicyShrimps
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ToDo: Add code to run seed method here instead of startup like previously



            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
