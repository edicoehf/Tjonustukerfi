using System.Collections.Generic;
using System.Linq;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Extensions;
using ThjonustukerfiWebAPI.Config;

namespace ThjonustukerfiWebAPI.Setup
{
    public class BaseSetup : IBaseSetup
    {
        private DataContext _dbContext;
        private List<Service> _services;
        private List<ServiceState> _serviceStates;
        private List<State> _states;
        private List<Category> _categories;

        public BaseSetup(DataContext context)
        {
            _dbContext = context;
        }

        // setup Service
        public void Run(ConfigClass config)
        {
            LoadConfig(config);

            var DB_Services = _dbContext.Set<Service>().ToList();
            var DB_ServiceStates = _dbContext.Set<ServiceState>().ToList();
            var DB_States = _dbContext.Set<State>().ToList();
            var DB_Categories = _dbContext.Set<Category>().ToList();

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
            Constants.sendEmail = config.SendEmail;                                 // Set email option
            Constants.sendSMS = config.SendSMS;                                     // Set SMS option
            Constants.Locations = config.Locations;                                 // Set item possible locations
            Constants.CompanyEmail = config.CompanyEmail;                           // Set the company email
            Constants.CompanyName = config.CompanyName;                             // Set the company name
            BarcodeImageDimensions.Height = config.BarcodeImageDimensions.Height;   // Set barcode image height
            BarcodeImageDimensions.Width = config.BarcodeImageDimensions.Width;     // Set barcode image width

            _services = config.Services;            // Set services
            _serviceStates = config.ServiceStates;  // Set service states
            _states = config.States;                // Set states
            _categories = config.Categories;        // Set categories
        }
    }
}