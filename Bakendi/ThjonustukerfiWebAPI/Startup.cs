using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ThjonustukerfiWebAPI.Setup;
using ThjonustukerfiWebAPI.Extensions;
using ThjonustukerfiWebAPI.Mappings;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Repositories.Interfaces;
using ThjonustukerfiWebAPI.Services.Implementations;
using ThjonustukerfiWebAPI.Services.Interfaces;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using FluentScheduler;
using ThjonustukerfiWebAPI.Schedules;
using Microsoft.Extensions.Logging;

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
			if(connectionString == null)
			{
				connectionString = Configuration.GetConnectionString("DefaultConnection");
			}

			// Special case for Heroku deployment: db connection may be specified via the DATABASE_URL config value,
			// which needs to be parsed separately.
			var herokuDatabaseUrl = Configuration["DATABASE_URL"];
			if (!string.IsNullOrEmpty(herokuDatabaseUrl))
			{
				var builder = new PostgreSqlConnectionStringBuilder(herokuDatabaseUrl)
				{
					Pooling                = true,
					TrustServerCertificate = true,
					SslMode                = SslMode.Require
				};
				connectionString = builder.ConnectionString;
			}

			services.AddDbContext<DataContext>(options => 
			{
				options.UseNpgsql(connectionString);
				
			});

			// set the constant connection string for other services to connect
			Constants.DBConnection = connectionString;

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

			// Adding for Order
			services.AddTransient<IOrderService, OrderService>();
			services.AddTransient<IOrderRepo, OrderRepo>();
			// Adding for SetupTables
			services.AddTransient<IBaseSetup, BaseSetup>();

			// Adding for Information
			services.AddTransient<IInfoService, InfoService>();
			services.AddTransient<IInfoRepo, InfoRepo>();

			//* Swagger Documentation
			services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc("v1",
				new OpenApiInfo
				{
					Title = "Þjónustukerfi Edico Bakendi",
					Version = "v1"
				});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				opt.IncludeXmlComments(xmlPath, includeControllerXmlComments : true);
			});

			JobManager.Initialize(new Scheduler());
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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
			
			//* Use swagger
			app.UseSwagger();
			app.UseSwaggerUI(opt =>
			{
				opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Þjónustukerfi Edico Bakendi");
			});

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			// Add log4Net middleware
			var logConfigPath = $"{AppContext.BaseDirectory}" + @"Config/log4net.config";
			loggerFactory.AddLog4Net(logConfigPath);
		}
	}
}
