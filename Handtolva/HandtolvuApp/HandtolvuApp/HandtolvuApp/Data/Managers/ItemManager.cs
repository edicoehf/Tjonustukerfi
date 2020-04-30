using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.Data
{
    public class ItemManager
    {
        readonly IItemService itemService;
        
        public ItemManager(IItemService service)
        {
            itemService = service;
        }

        public Task<Item> GetItemAsync(string barcode)
        {
            return itemService.GetItemAsync(barcode);
        }

        public Task<NextStates> GetNextStatesAsync(string barcode)
        {
            return itemService.GetNextStatesAsync(barcode);
        }

        public Task<bool> StateChangeWithId(long itemId, string barcode)
        {
            return itemService.StateChangeWithId(itemId, barcode);
        }

        public Task StateChangeByLocation(ObservableCollection<string> items, string barcode)
        {
            return itemService.StateChangeByLocation(items, barcode);
        }
    }
}
