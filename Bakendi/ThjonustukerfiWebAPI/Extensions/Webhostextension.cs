using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThjonustukerfiWebAPI.Setup;
using ThjonustukerfiWebAPI.Models;
using System.Xml;
using System.IO;
using System.Reflection;

namespace ThjonustukerfiWebAPI.Extensions
{
    /// <summary>
        ///     Extends the WebHost application with custom functionality.
        ///     
        ///     All functions take in the WebHost application as a parameter and always return it.
        /// </summary>
    public static class WebhostExtension
    {
        // Maybe use later...
        public static IHost MigrateDatabase<T>(this IHost webHost) where T:DbContext
        {
            var serviceScopeFactory =   (IServiceScopeFactory)webHost
                                        .Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<T>();

                dbContext.Database.Migrate();
            }

            return webHost;
        }

        /// <summary>
        ///     Fills the tables required for the companies service.
        ///     
        ///     If there is no change, this will do nothing.
        /// </summary>
        public static IHost FillTables(this IHost webHost)
        {
            var serviceScopeFactory =   (IServiceScopeFactory)webHost
                                        .Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                ISetupTables setuptable = new SetupTables(services.GetRequiredService<DataContext>());

                setuptable.Run();
            }

            return webHost;
        }

        /// <summary>
        ///     Sets up log4net via the log4net config file.
        /// </summary>
        public static IHost Log4NetSetup(this IHost webHost)
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(@"Config\log4net.config"));
            var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            return webHost;
        }
    }
}