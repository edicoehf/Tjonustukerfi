using System.Collections;
using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    /// <summary>Service functions for Orders.</summary>
    public interface IOrderService
    {
        /// <summary>Gets an order with the given ID.</summary>
        /// <returns>An order DTO.</returns>
        OrderDTO GetOrderById(long id);

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

        IEnumerable GetAllRawOrders();

        /// <summary>Sets all items to complete in order</summary>
        void CompleteOrder(long orderId);

        /// <summary>Gets the order with the given barcode</summary>
        OrderDTO SearchOrder(string barcode);

        /// <summary>Removes the order with the given barcode</summary>
        void RemoveOrderQuery(string barcode);

        /// <summary>Gets all item information from order for printing.</summary>
        /// <returns>List of item print information and image of barcode as a byte array.</returns>
        List<ItemPrintDetailsDTO> GetOrderPrintDetails(long orderId);

        /// <summary>Sends all item barcode images in an order to the customer that owns the order, used for demo purposes.</summary>
        void SendOrderBarcodeByEmail(long id);
    }
}