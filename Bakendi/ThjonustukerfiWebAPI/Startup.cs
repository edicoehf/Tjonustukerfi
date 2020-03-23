using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ThjonustukerfiWebAPI.Configurations;
using ThjonustukerfiWebAPI.Extensions;
using ThjonustukerfiWebAPI.Mappings;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Implementations;
using ThjonustukerfiWebAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace ThjonustukerfiWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        readonly string MyAllowSpecificOrigins = "AllowOrigins";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // CORS
            services.AddCors(c =>
            {
                c.AddPolicy(MyAllowSpecificOrigins, options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // Adding database connection
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            services.AddDbContext<DataContext>(options => {
                if (connectionString != null)
                {
                    options.UseNpgsql(connectionString);
                }
                else
                {
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                }
            });

            // Adding automapper
            var mappingProfile = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingProfile.CreateMapper();
            services.AddSingleton(mapper);

            //* Adding Interfaces for dependency injections
            // Adding for Customer
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepo, CustomerRepo>();

            // Adding for Item
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IItemRepo, ItemRepo>();

            // Adding for Log Repository
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<ILogRepository, LogRepository>();

            // Adding foor SetupTables
            services.AddTransient<ISetupTables, SetupTables>();

            //* Swagger Documentation
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1",
                new Info
                {
                    Title = "Þjónustukerfi Edico Bakendi",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = xmlPath.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);

            // app.UseHttpsRedirection();

            // Exception Middleware, new exceptions must be added to exception folder in models
            // and then implemented in the middleware extension
            app.UseMiddleware<ExceptionMiddlewareExtension>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Use swagger
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Þjónustukerfi Edico Bakendi");
            });
        }
    }
}
