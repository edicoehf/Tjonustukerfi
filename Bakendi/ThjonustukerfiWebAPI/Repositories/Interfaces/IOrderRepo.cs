using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
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
    }
}