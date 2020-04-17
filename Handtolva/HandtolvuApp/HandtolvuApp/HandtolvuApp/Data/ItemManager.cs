using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.Data
{
    public class ItemManager
    {
        IRestService restService;
        IItemService itemService;
        
        public ItemManager(IRestService service, IItemService itemservice)
        {
            restService = service;
            this.itemService = itemservice;
        }

        public Task<Item> GetItemAsync(string barcode)
        {
            return restService.GetItemAsync(barcode);
        }

        public Task<Order> GetOrderAsync(string barcode)
        {
            return restService.GetOrderAsync(barcode);
        }

        public Task CheckoutOrder(long id)
        {
            return restService.CheckoutOrder(id);
        }

        public Task<NextStates> GetNextStatesAsync(string barcode)
        {
            return itemService.GetNextStatesAsync(barcode);
        }
    }
}
