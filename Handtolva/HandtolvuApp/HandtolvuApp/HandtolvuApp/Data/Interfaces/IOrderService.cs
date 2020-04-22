using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.Data.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetOrderAsync(string barcode);
        Task CheckoutOrder(long id);
    }
}
