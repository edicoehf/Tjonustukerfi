using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface IInfoRepo
    {
        /// <summary>Gets all services available.</summary>
        /// <returns>A list of all available services.</returns>
        IEnumerable<ServiceDTO> GetServices();

        /// <summary>Gets all states available for the product/service</summary>
        /// <returns>A list of all available services.</returns>
        IEnumerable<StateDTO> GetStates();

        /// <summary>Gets all categories of items</summary>
        /// <returns>A list of all categories</returns>
        IEnumerable<CategoryDTO> GetCategories();
    }
}