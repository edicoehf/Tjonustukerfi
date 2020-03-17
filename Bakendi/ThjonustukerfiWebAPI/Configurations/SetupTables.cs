using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ThjonustukerfiWebAPI.Configurations;
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

        public SetupTables(DataContext context)
        {
            _dbContext = context;
        }

        // setup Service
        public void Run()
        {
            var DB_Services = _dbContext.Set<Service>().ToList();
            var DB_ServiceStates = _dbContext.Set<ServiceState>().ToList();
            var DB_States = _dbContext.Set<State>().ToList();

            fillLists();
            bool change = false;

            // empty or not with all data
            if(DB_Services == null || DB_Services.Count == 0 || DB_Services.Count < _services.Count)
            {
                _services.RemovExisting(DB_Services);
                _dbContext.Service.AddRange(_services);

                change = true;
            }

            // empty or not with all data
            if(DB_ServiceStates == null || DB_ServiceStates.Count == 0 || DB_ServiceStates.Count < _serviceStates.Count)
            {
                _serviceStates.RemovExisting(DB_ServiceStates);
                _dbContext.ServiceState.AddRange(_serviceStates);

                change = true;
            }
            
            // empty or not with all data
            if(DB_States == null || DB_States.Count == 0 || DB_States.Count < _states.Count)
            {
                _states.RemovExisting(DB_States);
                _dbContext.State.AddRange(_states);

                change = true;
            }

            if(change) { _dbContext.SaveChanges(); }
        }

        private void fillLists()
        {
            //* Add services here
            _services = new List<Service>()
            {
                // Birkireyking
                new Service() {Name = "Birkireyking", Id = 1}
            };

            //* Add Service states here
            _serviceStates = new List<ServiceState>()
            {
                // Service state fyrir birkireykingu
                new ServiceState() {Id = 1, ServiceId = 1, StateId = 1, Step = 1},
                new ServiceState() {Id = 2, ServiceId = 1, StateId = 2, Step = 2},
                new ServiceState() {Id = 3, ServiceId = 1, StateId = 3, Step = 2},
                new ServiceState() {Id = 4, ServiceId = 1, StateId = 4, Step = 2},
                new ServiceState() {Id = 5, ServiceId = 1, StateId = 5, Step = 3}
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
        }
    }
}