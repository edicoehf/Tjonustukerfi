using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.DTOs;
using ThjonustukerfiWebAPI.Models.InputModels;

namespace ThjonustukerfiWebAPI.Repositories.Interfaces
{
    public interface IOrderRepo
    {
        OrderDTO GetOrderbyId(long id);
        long CreateOrder(OrderInputModel order);
        void UpdateOrder(OrderInputModel order, long id);
        void DeleteByOrderId(long id);
        IEnumerable<OrderDTO> GetAllOrders();
        ItemStateDTO SearchItem(string search);
    }
}