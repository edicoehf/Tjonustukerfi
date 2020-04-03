using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface IInfoRepo
    {
        IEnumerable<ServiceDTO> GetServices();

        /// <summary>Gets all states available for the product/service</summary>
        IEnumerable<StateDTO> GetStates();
    }
}