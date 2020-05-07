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
                .Setup("Reykofninn")    // custom function, send in the name of the company, must be the same as the 
                                        // name of the config file (Minus Config), e.g. "Reykofninn" where filename is "ReykofninnConfig.json"
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
