using System;
using ThjonustukerfiWebAPI.Models;
using ThjonustukerfiWebAPI.Models.Entities;
using ThjonustukerfiWebAPI.Models.Exceptions;
using ThjonustukerfiWebAPI.Repositories.Interfaces;

namespace ThjonustukerfiWebAPI.Repositories.Implementations
{
    public class LogRepository : ILogRepository
    {
        private DataContext _dbContext;
        public LogRepository(DataContext context)
        {
            _dbContext = context;
        }

        public void LogToDatabase(ExceptionModel exception)
        {
            _dbContext.ExceptionLog.Add(new Log
            {
                ExceptionMessage = exception.Message,
                StatusCode = exception.StatusCode,
                StackTrace = exception.StackTrace,
                DateOfError = DateTime.Now
            });
            _dbContext.SaveChanges();
        }
    }
}