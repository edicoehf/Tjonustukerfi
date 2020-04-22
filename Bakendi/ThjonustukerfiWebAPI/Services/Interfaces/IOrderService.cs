using System.Collections;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    /// <summary>Service functions for Orders.</summary>
    public interface IOrderService
    {
        /// <summary>Gets an order with the given ID.</summary>
        /// <returns>An order DTO.</returns>
        OrderDTO GetOrderbyId(long id);

        /// <summary>Creates an order with the given input.</summary>
        /// <returns>The id of the newly created entity.</returns>
        long CreateOrder(OrderInputModel order);

        /// <summary>Updates an order with the given input and ID.</summary>
        void UpdateOrder(OrderInputModel order, long id);

        /// <summary>Deletes an order with the given ID.</summary>
        void DeleteByOrderId(long id);

        /// <summary>Gets all orders.</summary>
        /// <returns>A list of all orders.</returns>
        IEnumerable GetAllOrders();

        /// <summary>Sets all items to complete in order</summary>
        void CompleteOrder(long orderId);

        /// <summary>Gets the order with the given barcode</summary>
        OrderDTO SearchOrder(string barcode);

        /// <summary>Removes the order with the given barcode</summary>
        void RemoveOrderQuery(string barcode);
    }
}