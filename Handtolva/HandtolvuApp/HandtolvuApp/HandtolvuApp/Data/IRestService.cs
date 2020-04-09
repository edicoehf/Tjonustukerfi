using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.Data
{
    public interface IRestService
    {
        Task<Item> GetItemAsync(string barcode);
        Task<Order> GetOrderAsync(string barcode);
        Task CheckoutOrder(string barcode);
    }
}
