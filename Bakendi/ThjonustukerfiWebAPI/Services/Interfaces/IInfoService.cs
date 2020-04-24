using System.Collections;
using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    /// <summary>Service functions for Information.</summary>
    public interface IInfoService 
    {
        /// <summary>Gets all services available.</summary>
        /// <returns>A list of services.</returns>
        IEnumerable GetServices();

        /// <summary>Gets all states available for the product/service</summary>
        /// <returns>A list of all states.</returns>
        IEnumerable GetStates();

        /// <summary>Gets all categories of items</summary>
        /// <returns>A list of all categories</returns>
        IEnumerable GetCategories();

        /// <summary>Gets all Archived orders along with their Items</summary>
        /// <returns>A list of all archived orders</returns>
        List<ArchiveOrderDTO> GetArchivedOrders();
    }
}