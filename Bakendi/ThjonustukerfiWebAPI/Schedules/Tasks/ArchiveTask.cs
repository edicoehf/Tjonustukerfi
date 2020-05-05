using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentScheduler;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ThjonustukerfiWebAPI.Setup;
using ThjonustukerfiWebAPI.Mappings;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Repositories.Implementations;
using ThjonustukerfiWebAPI.Repositories.Interfaces;

namespace ThjonustukerfiWebAPI.Schedules.Tasks
{
    /// <summary>Scheduled task to archive orders that have been completed for 3 months or more.</summary>
    public class ArchiveTask : IJob
    {
        private IOrderRepo _orderRepo;

        // This constructor is called each time the task is run, the context will then be created, used and then destroyed
        public ArchiveTask()
        {
            DataContext dbContext;
            // Create database connection
            var options = new DbContextOptionsBuilder<DataContext>().UseNpgsql(Constants.DBConnection).Options;
            dbContext = new DataContext(options);

            Mapper mapper;
            // Create the mapping profile and the mapper
            var profile = new MappingProfile();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            mapper = new Mapper(config);

            // create the repo and inject the context and mapper
            _orderRepo = new OrderRepo(dbContext, mapper);
        }

        // Execution of the task
        public void Execute()
        {
            _orderRepo.ArchiveOldOrders();
        }
    }
}