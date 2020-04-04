using ThjonustukerfiWebAPI.Models.Exceptions;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    public interface ILogService
    {
        /// <summary>Logs the exception.</summary>
        void LogToDatabase(ExceptionModel exception);
    }
}