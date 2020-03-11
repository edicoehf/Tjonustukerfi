using ThjonustukerfiWebAPI.Models.Exceptions;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface ILogRepository
    {
        void LogToDatabase(ExceptionModel exception);
    }
}