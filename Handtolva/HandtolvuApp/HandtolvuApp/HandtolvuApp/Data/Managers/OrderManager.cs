using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.Data.Managers
{
    public class OrderManager
    {
        readonly IOrderService orderService;

        public OrderManager(IOrderService service)
        {
            orderService = service;
        }

        /// <summary>Gets a given order</summary>
        /// <param name="barcode">barcode of the given order</param>
        /// <returns>Returns a Order model</returns>
        public Task<Order> GetOrderAsync(string barcode)
        {
            return orderService.GetOrderAsync(barcode);
        }

        /// <summary>
        ///     Marks all item in order as "Sótt"
        /// </summary>
        /// <param name="id">id of the order to checkout</param>
        /// <returns>Boolean for success or failure</returns>
        public Task<bool> CheckoutOrder(long id)
        {
            return orderService.CheckoutOrder(id);
        }
    }
}
