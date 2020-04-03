using System.Collections;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    public interface IInfoService 
    {
        IEnumerable GetServices();

        /// <summary>Gets all states available for the product/service</summary>
        IEnumerable GetStates();
    }
}