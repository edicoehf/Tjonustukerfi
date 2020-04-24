using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    /// <summary>Repository for accessing the Database for Orders.</summary>
    public interface IOrderRepo
    {
        /// <summary>Gets an order with the given ID from the database.</summary>
        /// <returns>An OrderDTO object.</returns>
        OrderDTO GetOrderbyId(long id);

        /// <summary>Creates a new order and adds it to the database.</summary>
        /// <returns>The ID of the entity created.</returns>
        long CreateOrder(OrderInputModel order);

        /// <summary>Updates an order in the database.</summary>
        void UpdateOrder(OrderInputModel order, long id);

        /// <summary>Deletes an order with the given ID from the database.</summary>
        void DeleteByOrderId(long id);

        /// <summary>Gets all orders in the database.</summary>
        /// <returns>A list of OrderDTO.</returns>
        IEnumerable<OrderDTO> GetAllOrders();

        /// <summary>Sets all items to complete in order</summary>
        /// <returns>OrderDTO of the order set to complete</returns>
        OrderDTO CompleteOrder(long orderId);

        /// <summary>Finds ID of order with the given barcode.</summary>
        /// <returns>The orders ID</returns>
        long SearchOrder(string barcode);

        /// <summary>Gets a list of all active orders with the customer</summary>
        /// <returns>A list of active orders, empty if no orders exist that are active</returns>
        List<OrderDTO> GetActiveOrdersByCustomerId(long customerID);

        /// <summary>Function to archive orders that have been completed for 3 months or more</summary>
        void ArchiveOldOrders();

        /// <summary>Function to archive orders by ID that are complete</summary>
        void ArchiveCompleteOrdersByCustomerId(long customerId);
    }
}