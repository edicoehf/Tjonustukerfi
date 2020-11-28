using System.Collections.Generic;
using System.Linq;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Extensions;
using ThjonustukerfiWebAPI.Config;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using ThjonustukerfiWebAPI.Config.EnvironmentVariables;

namespace ThjonustukerfiWebAPI.Setup
{
    public class BaseSetup : IBaseSetup
    {
        private DataContext _dbContext;
        private List<Service> _services;
        private List<ServiceState> _serviceStates;
        private List<State> _states;
        private List<Category> _categories;

        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(BaseSetup));

        public BaseSetup(DataContext context)
        {
            _dbContext = context;
        }

        // setup Service
        public void Run(ConfigClass config)
        {
            LoadConfig(config);
            SetEnvironmentVariables();  // set environment variables for docker

            _dbContext.Database.Migrate();

            var DB_Services      = _dbContext.Set<Service>().ToList();
            var DB_ServiceStates = _dbContext.Set<ServiceState>().ToList();
            var DB_States        = _dbContext.Set<State>().ToList();
            var DB_Categories    = _dbContext.Set<Category>().ToList();

            bool change = false;

            // empty or not with all data
            if(DB_Services == null || DB_Services.Count == 0 || !_services.ContainsSameElements(DB_Services))
            {
                var removeItems = _services.GetNotSame(DB_Services);    // check if any services have been removed from the config
                if(removeItems.Count > 0) { _dbContext.Service.RemoveRange(removeItems); }  // if so remove those services

                _services.RemoveExisting(DB_Services);      // Removes all services that are the same so we only add new services
                _dbContext.Service.AddRange(_services);

                change = true;
            }

            // empty or not with all data
            if(DB_ServiceStates == null || DB_ServiceStates.Count == 0 || !_serviceStates.ContainsSameElements(DB_ServiceStates))
            {
                var removeItems = _serviceStates.GetNotSame(DB_ServiceStates);  // check if any service states have been removed from the config
                if(removeItems.Count > 0) { _dbContext.ServiceState.RemoveRange(removeItems); } // if so, remove them

                _serviceStates.RemoveExisting(DB_ServiceStates);    // Removes all servicestates that are the same so we only add new service states
                _dbContext.ServiceState.AddRange(_serviceStates);

                change = true;
            }
            
            // empty or not with all data
            if(DB_States == null || DB_States.Count == 0 || !_states.ContainsSameElements(DB_States))
            {
                var removeItems = _states.GetNotSame(DB_States);    // checks if any States have been removed from the config
                if(removeItems.Count > 0) { _dbContext.State.RemoveRange(removeItems); }    // if so, remove them

                _states.RemoveExisting(DB_States);  // Removes all states that are the same so we only add new service states
                _dbContext.State.AddRange(_states);

                change = true;
            }

            // empty or not with all data
            if(DB_Categories == null || DB_Categories.Count == 0 || !_categories.ContainsSameElements(DB_Categories))
            {
                var removeItems = _categories.GetNotSame(DB_Categories);    // checks if any categories have benn removed from the config
                if(removeItems.Count > 0) { _dbContext.Category.RemoveRange(removeItems); } // if so, remove them

                _categories.RemoveExisting(DB_Categories);  // Removes all categories that are the same so we only add new categories
                _dbContext.Category.AddRange(_categories);

                change = true;
            }

            if(change) { _dbContext.SaveChanges(); }
        }

        /// <summary>Fills all config variables that need to be set.</summary>
        private void LoadConfig(ConfigClass config)
        {
            Constants.sendEmail              = config.SendEmail;                      // Set email option
            Constants.sendSMS                = config.SendSMS;                        // Set SMS option
            Constants.Locations              = config.Locations;                      // Set item possible locations
            Constants.CompanyEmail           = config.CompanyEmail;                   // Set the company email
            Constants.CompanyCc              = config.CompanyCc;
            Constants.CompanyEmailDisclaimer = config.CompanyEmailDisclaimer;
            Constants.CompanyName            = config.CompanyName;                    // Set the company name
            BarcodeImageDimensions.Height    = config.BarcodeImageDimensions.Height;  // Set barcode image height
            BarcodeImageDimensions.Width     = config.BarcodeImageDimensions.Width;   // Set barcode image width

            _services      = config.Services;
            _serviceStates = config.ServiceStates;
            _states        = config.States;
            _categories    = config.Categories;
        }

        /// <summary>
        ///     Sets Environment files for mail service, also updates them if there were any changes. Mainly used for docker.
        ///     
        ///     In production you should have these variables already in the .env file. You can also set your environment variables
        ///     on your operating system. This will also update the env file given that the variable names are correct.
        /// </summary>
        private void SetEnvironmentVariables()
        {
            // .env path
            string envPath = "Config/EnvironmentVariables/.env";

            // get environment variables
            string SMTP_USERNAME = Environment.GetEnvironmentVariable("SMTP_USERNAME");
            string SMTP_PASSWORD = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            string SMTP_SERVER   = Environment.GetEnvironmentVariable("SMTP_SERVER");
            string SMTP_PORT     = Environment.GetEnvironmentVariable("SMTP_PORT");

            string SMS_USER      = Environment.GetEnvironmentVariable("SMS_USER");
            string SMS_PASS      = Environment.GetEnvironmentVariable("SMS_PASS");
            string SMS_URL       = Environment.GetEnvironmentVariable("SMS_URL");
            string SMS_BACKEND   = Environment.GetEnvironmentVariable("SMS_BACKEND");

            // if any of these variables are not set, then do not continue
            if(SMTP_USERNAME == null || SMTP_PASSWORD == null || SMTP_SERVER == null || SMTP_PORT == null) { return; }

            // if the file doesn't exist, use the .env variables
            if(!File.Exists(envPath))
            {
                _log.Error(".env file doesn't exist, will create it from scratch");

                var path = "Config/EnvironmentVariables";

                System.IO.Directory.CreateDirectory(path);

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, ".env")))
                {
                    outputFile.WriteLine($"SMTP_USERNAME={SMTP_USERNAME}");
                    outputFile.WriteLine($"SMTP_PASSWORD={SMTP_PASSWORD}");
                    outputFile.WriteLine($"SMTP_SERVER={SMTP_SERVER}");
                    outputFile.WriteLine($"SMTP_PORT={SMTP_PORT}");

                    outputFile.WriteLine($"SMS_USER={SMS_USER}");
                    outputFile.WriteLine($"SMS_PASS={SMS_PASS}");
                    outputFile.WriteLine($"SMS_URL={SMS_URL}");
                    outputFile.WriteLine($"SMS_BACKEND={SMS_BACKEND}");
                }
            }
            else    // if it does exist, check if the environment variables have changed
            {
                _log.Error(".env file DOES exist, check if it needs updating");

                var envVar = EnvironmentFileManager.LoadEvironmentFile();

                string smtpUsername, smtpPassword, smtpServer, smtpPort;
                string smsUser, smsPass, smsUrl, smsBackend;
                envVar.TryGetValue("SMTP_USERNAME", out smtpUsername);
                envVar.TryGetValue("SMTP_PASSWORD", out smtpPassword);
                envVar.TryGetValue("SMTP_SERVER",   out smtpServer);
                envVar.TryGetValue("SMTP_PORT",     out smtpPort);
                envVar.TryGetValue("SMS_USER",      out smsUser);
                envVar.TryGetValue("SMS_PASS",      out smsPass);
                envVar.TryGetValue("SMS_URL",       out smsUrl);
                envVar.TryGetValue("SMS_BACKEND",   out smsBackend);

                // Reset env file if any values are not equal or set
                if(SMTP_USERNAME != smtpUsername 
                || SMTP_PASSWORD != smtpPassword 
                || SMTP_SERVER   != smtpServer
                || SMTP_PORT     != smtpPort
                || SMS_USER      != smsUser
                || SMS_PASS      != smsPass
                || SMS_URL       != smsUrl
                || SMS_BACKEND   != smsBackend)
                {
                    // Not really an error though...
                    _log.Error("Some value in the env file does not match with the environment variables, will write new values to env file");

                    File.WriteAllText(envPath, string.Empty);

                    var path = "Config/EnvironmentVariables";
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, ".env")))
                    {
                        outputFile.WriteLine($"SMTP_USERNAME={SMTP_USERNAME}");
                        outputFile.WriteLine($"SMTP_PASSWORD={SMTP_PASSWORD}");
                        outputFile.WriteLine($"SMTP_SERVER={SMTP_SERVER}");
                        outputFile.WriteLine($"SMTP_PORT={SMTP_PORT}");

                        outputFile.WriteLine($"SMS_USER={SMS_USER}");
                        outputFile.WriteLine($"SMS_PASS={SMS_PASS}");
                        outputFile.WriteLine($"SMS_URL={SMS_URL}");
                        outputFile.WriteLine($"SMS_BACKEND={SMS_BACKEND}");
                    }
                }
            }
        }
    }
}