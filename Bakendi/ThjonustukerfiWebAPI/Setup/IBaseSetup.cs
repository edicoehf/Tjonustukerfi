using ThjonustukerfiWebAPI.Config;

namespace ThjonustukerfiWebAPI.Setup
{
    /// <summary>
    ///     Setup all tables for the company with the company config. Also sets up constants and environmental variables.
    /// </summary>
    public interface IBaseSetup
    {
        /// <summary>
        ///     Checks whether all services are setup up, used before running the WebHost application after building it
        /// </summary>
        void Run(ConfigClass config);
    }
}