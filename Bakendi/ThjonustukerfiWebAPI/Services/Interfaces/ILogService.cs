using ThjonustukerfiWebAPI.Models.Exceptions;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    /// <summary>Service functions for Logging exceptions to the database.</summary>
    public interface ILogService
    {
        /// <summary>Logs the exception.</summary>
        void LogToDatabase(ExceptionModel exception);
    }
}