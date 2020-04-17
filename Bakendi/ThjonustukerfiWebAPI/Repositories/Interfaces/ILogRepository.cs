using ThjonustukerfiWebAPI.Models.Exceptions;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    /// <summary>Repository for accessing the Database for Exception logs.</summary>
    public interface ILogRepository
    {
        /// <summary>Logs the exception to the database.</summary>
        void LogToDatabase(ExceptionModel exception);
    }
}