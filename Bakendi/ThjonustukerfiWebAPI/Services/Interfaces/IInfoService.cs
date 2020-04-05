using System.Collections;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    public interface IInfoService 
    {
        /// <summary>Gets all services available.</summary>
        /// <returns>A list of services.</returns>
        IEnumerable GetServices();

        /// <summary>Gets all states available for the product/service</summary>
        /// <returns>A list of all states.</returns>
        IEnumerable GetStates();
    }
}