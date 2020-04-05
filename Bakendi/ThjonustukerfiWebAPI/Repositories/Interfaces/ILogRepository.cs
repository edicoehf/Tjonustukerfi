using ThjonustukerfiWebAPI.Models.Exceptions;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface ILogRepository
    {
        /// <summary>Logs the exception to the database.</summary>
        void LogToDatabase(ExceptionModel exception);
    }
}