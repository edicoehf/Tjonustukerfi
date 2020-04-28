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
        IOrderService orderService;

        public OrderManager(IOrderService service)
        {
            orderService = service;
        }

        public Task<Order> GetOrderAsync(string barcode)
        {
            return orderService.GetOrderAsync(barcode);
        }

        public Task CheckoutOrder(long id)
        {
            return orderService.CheckoutOrder(id);
        }
    }
}
