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
        
        public ItemManager(IRestService service)
        {
            restService = service;
        }

        public Task<Item> GetItemAsync(string barcode)
        {
            return restService.GetItemAsync(barcode);
        }

        public Task CheckoutOrder(string barcode)
        {
            return restService.CheckoutOrder(barcode);
        }
    }
}
