using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentScheduler;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ThjonustukerfiWebAPI.Configurations;
using ThjonustukerfiWebAPI.Models;

namespace ThjonustukerfiWebAPI.Schedules.Tasks
{
    /// <summary>Scheduled task to archive orders</summary>
    public class ArchiveTask : IJob
    {
        private DataContext _dbContext;

        // This constructor is called each time the task is run, the context will then be created, used and then destroyed
        public ArchiveTask()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseNpgsql(Constants.DBConnection).Options;
            _dbContext = new DataContext(options);
        }

        // Execution of the task
        public void Execute()
        {
            
        }
    }
}