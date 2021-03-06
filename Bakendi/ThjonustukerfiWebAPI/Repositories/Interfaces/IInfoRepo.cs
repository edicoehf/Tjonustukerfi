using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    /// <summary>Repository for accessing the Database for Information.</summary>
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

        /// <summary>Gets state by state ID</summary>
        /// <returns>A single State DTO</returns>
        StateDTO GetStatebyId(long id);

        /// <summary>Gets the next available states with the service ID and the current state ID</summary>
        /// <returns>A list of StateDTOs. Empty list if item is in final state</returns>
        List<StateDTO> GetNextStates(long serviceId, long stateId);

        /// <summary>Gets all Archived orders along with their Items</summary>
        /// <returns>A list of all archived orders</returns>
        List<ArchiveOrderDTO> GetArchivedOrders();

        /// <summary>Gets a list of an Items history</summary>
        /// <returns>List of ItemTimeStampDTO</returns>
        List<ItemTimeStampDTO> GetItemHistory(long itemId);
    }
}