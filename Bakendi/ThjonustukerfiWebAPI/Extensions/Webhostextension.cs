using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThjonustukerfiWebAPI.Setup;
using ThjonustukerfiWebAPI.Models;
using System.Xml;
using System.IO;
using System.Reflection;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ThjonustukerfiWebAPI.Config;

namespace ThjonustukerfiWebAPI.Extensions
{
    /// <summary>
        ///     Extends the WebHost application with custom functionality.
        ///     
        ///     All functions take in the WebHost application as a parameter and always return it.
        /// </summary>
    public static class WebhostExtension
    {
        /// <summary>
        ///     Fills the tables required for the companies service. As well as various company information from the companies config file.
        ///     
        ///     If there is no change, this will do nothing.
        /// </summary>
        public static IHost Setup(this IHost webHost, string company)
        {
            var serviceScopeFactory =   (IServiceScopeFactory)webHost
                                        .Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                IBaseSetup baseSetup = new BaseSetup(services.GetRequiredService<DataContext>());

                var path = $"{AppContext.BaseDirectory}Config/{company}Config.json";    // get the path
                var json = File.ReadAllText(path);                                      // read the file to json
                var config = JsonConvert.DeserializeObject<ConfigClass>(json);          // Convert to config class for easy setup

                baseSetup.Run(config);
            }

            return webHost;
        }
    }
}