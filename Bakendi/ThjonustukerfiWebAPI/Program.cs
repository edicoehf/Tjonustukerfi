using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Extensions;

namespace ThjonustukerfiWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .FillTables()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
