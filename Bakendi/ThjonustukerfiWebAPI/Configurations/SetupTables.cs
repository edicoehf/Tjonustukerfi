using System.Collections.Generic;
using System.Linq;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Extensions;

namespace ThjonustukerfiWebAPI.Configurations
{
    public class SetupTables : ISetupTables
    {
        private DataContext _dbContext;
        private List<Service> _services;
        private List<ServiceState> _serviceStates;
        private List<State> _states;
        private List<Category> _categories;

        public SetupTables(DataContext context)
        {
            _dbContext = context;
        }

        // setup Service
        public void Run()
        {
            //TODO: for config, maybe put in another class
            Constants.sendEmail = true;
            Constants.sendSMS = false;
            Constants.Locations = new List<string>()
            {
                "holf1", "holf2", "holf3", "holf4", "holf5",
                "holf6", "holf7", "holf8", "holf9", "holf10",
            };
            Constants.BarcodeImageSize = new BarcodeImageDimensions() { Width = 100, Height = 50 };

            var DB_Services = _dbContext.Set<Service>().ToList();
            var DB_ServiceStates = _dbContext.Set<ServiceState>().ToList();
            var DB_States = _dbContext.Set<State>().ToList();
            var DB_Categories = _dbContext.Set<Category>().ToList();

            fillLists();
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

        /// <summary>Fills the list to add to database, extra services, states and service States should be added here</summary>
        private void fillLists()
        {
            //* Add services here
            _services = new List<Service>()
            {
                // Birkireyking
                new Service() {Name = "Birkireyking", Id = 1},
                new Service() {Name = "Taðreyking", Id = 2},
                new Service() {Name = "ViðarReyking", Id = 3},
                new Service() {Name = "Salt pækill", Id = 4},
                new Service() {Name = "Annað", Id = 5}
            };

            //* Add Service states here
            _serviceStates = new List<ServiceState>()
            {
                // Service state fyrir birkireykingu
                new ServiceState() {Id = 1, ServiceId = 1, StateId = 1, Step = 1},
                new ServiceState() {Id = 2, ServiceId = 1, StateId = 2, Step = 2},
                new ServiceState() {Id = 3, ServiceId = 1, StateId = 3, Step = 2},
                new ServiceState() {Id = 4, ServiceId = 1, StateId = 4, Step = 2},
                new ServiceState() {Id = 5, ServiceId = 1, StateId = 5, Step = 3},

                // Service state fyrir Taðreykingu
                new ServiceState() {Id = 6, ServiceId = 2, StateId = 1, Step = 1},
                new ServiceState() {Id = 7, ServiceId = 2, StateId = 2, Step = 2},
                new ServiceState() {Id = 8, ServiceId = 2, StateId = 3, Step = 2},
                new ServiceState() {Id = 9, ServiceId = 2, StateId = 4, Step = 2},
                new ServiceState() {Id = 10, ServiceId = 2, StateId = 5, Step = 3},

                // Service state fyrir viðarReykingu
                new ServiceState() {Id = 11, ServiceId = 3, StateId = 1, Step = 1},
                new ServiceState() {Id = 12, ServiceId = 3, StateId = 2, Step = 2},
                new ServiceState() {Id = 13, ServiceId = 3, StateId = 3, Step = 2},
                new ServiceState() {Id = 14, ServiceId = 3, StateId = 4, Step = 2},
                new ServiceState() {Id = 15, ServiceId = 3, StateId = 5, Step = 3},

                // Service state fyrir salt pækil
                new ServiceState() {Id = 16, ServiceId = 4, StateId = 1, Step = 1},
                new ServiceState() {Id = 17, ServiceId = 4, StateId = 2, Step = 2},
                new ServiceState() {Id = 18, ServiceId = 4, StateId = 3, Step = 2},
                new ServiceState() {Id = 19, ServiceId = 4, StateId = 4, Step = 2},
                new ServiceState() {Id = 20, ServiceId = 4, StateId = 5, Step = 3},

                // Service state fyrir annað
                new ServiceState() {Id = 21, ServiceId = 5, StateId = 1, Step = 1},
                new ServiceState() {Id = 22, ServiceId = 5, StateId = 2, Step = 2},
                new ServiceState() {Id = 23, ServiceId = 5, StateId = 3, Step = 2},
                new ServiceState() {Id = 24, ServiceId = 5, StateId = 4, Step = 2},
                new ServiceState() {Id = 25, ServiceId = 5, StateId = 5, Step = 3}
            };

            //* Add states here
            _states = new List<State>()
            {
                // states fyrir Reykofninn
                new State() {Name = "Vinnslu", Id = 1},
                new State() {Name = "Kælir1", Id = 2},
                new State() {Name = "Kælir2", Id = 3},
                new State() {Name = "Frystir", Id = 4},
                new State() {Name = "Sótt", Id = 5}
            };

            //* Add categories here
            _categories = new List<Category>()
            {
                // Categories for Reykofninn
                new Category() {Name = "Lax", Id = 1},
                new Category() {Name = "Silungur", Id = 2},
                new Category() {Name = "Lambakjöt", Id = 3},
                new Category() {Name = "Annað", Id = 4}
            };
        }
    }
}