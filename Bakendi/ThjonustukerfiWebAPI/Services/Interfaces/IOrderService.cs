using System.Collections;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Services.Interfaces
{
    public interface IOrderService
    {
        OrderDTO GetOrderbyId(long id);
        long CreateOrder(OrderInputModel order);
        void UpdateOrder(OrderInputModel order, long id);
        void DeleteByOrderId(long id);
        IEnumerable GetAllOrders();

        /// <summary>Searches for the given barcode</summary>
        ItemStateDTO SearchItem(string search);
    }
}