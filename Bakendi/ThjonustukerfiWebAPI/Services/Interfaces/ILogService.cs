using ThjonustukerfiWebAPI.Models.Exceptions;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    public interface ILogService
    {
        void LogToDatabase(ExceptionModel exception);
    }
}