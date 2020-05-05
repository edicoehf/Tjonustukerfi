using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ThjonustukerfiWebAPI.Extensions;

namespace ThjonustukerfiWebAPI
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .FillTables()   // custom function
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
