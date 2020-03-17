using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThjonustukerfiWebAPI.Configurations;
using ThjonustukerfiWebAPI.Models;

namespace ThjonustukerfiWebAPI.Extensions
{
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
    }
}