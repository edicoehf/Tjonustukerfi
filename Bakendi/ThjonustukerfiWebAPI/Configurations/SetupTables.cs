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
        //TODO: Add remove?
        public void Run()
        {
            var DB_Services = _dbContext.Set<Service>().ToList();
            var DB_ServiceStates = _dbContext.Set<ServiceState>().ToList();
            var DB_States = _dbContext.Set<State>().ToList();
            var DB_Categories = _dbContext.Set<Category>().ToList();

            fillLists();
            bool change = false;

            // empty or not with all data
            if(DB_Services == null || DB_Services.Count == 0 || DB_Services.Count != _services.Count)
            {
                _services.RemoveExisting(DB_Services);
                _dbContext.Service.AddRange(_services);

                change = true;
            }

            // empty or not with all data
            if(DB_ServiceStates == null || DB_ServiceStates.Count == 0 || DB_ServiceStates.Count != _serviceStates.Count)
            {
                _serviceStates.RemoveExisting(DB_ServiceStates);
                _dbContext.ServiceState.AddRange(_serviceStates);

                change = true;
            }
            
            // empty or not with all data
            if(DB_States == null || DB_States.Count == 0 || DB_States.Count != _states.Count)
            {
                _states.RemoveExisting(DB_States);
                _dbContext.State.AddRange(_states);

                change = true;
            }

            // empty or not with all data
            if(DB_Categories == null || DB_Categories.Count == 0 || DB_Categories.Count != _states.Count)
            {
                _categories.RemoveExisting(DB_Categories);
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
                new Service() {Name = "Salt pækill", Id = 4}
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
                new ServiceState() {Id = 20, ServiceId = 4, StateId = 5, Step = 3}
            };

            //* Add states here
            _states = new List<State>()
            {
                // states fyrir Reykofninn
                new State() {Name = "Í vinnslu", Id = 1},
                new State() {Name = "Kælir 1", Id = 2},
                new State() {Name = "Kælir 2", Id = 3},
                new State() {Name = "Frystir", Id = 4},
                new State() {Name = "Sótt", Id = 5}
            };

            //* Add categories here
            _categories = new List<Category>()
            {
                // Categories for Reykofninn
                new Category() {Name = "Lax", Id = 1},
                new Category() {Name = "Silungur", Id = 2},
                new Category() {Name = "Lambakjöt", Id = 3}
            };
        }
    }
}