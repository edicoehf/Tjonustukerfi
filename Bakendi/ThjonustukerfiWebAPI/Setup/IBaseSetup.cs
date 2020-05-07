using ThjonustukerfiWebAPI.Config;

namespace ThjonustukerfiWebAPI.Setup
{
    /// <summary>Sets up the database for services that the company provides</summary>
    public interface IBaseSetup
    {
        /// <summary>
        ///     Checks whether all services are setup up, used before running the WebHost application after building it
        /// </summary>
        void Run(ConfigClass config);
    }
}