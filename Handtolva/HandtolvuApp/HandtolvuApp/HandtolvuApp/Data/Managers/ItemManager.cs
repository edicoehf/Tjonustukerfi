﻿using HandtolvuApp.Data.Interfaces;
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
        IItemService itemService;
        
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

        public Task StateChangeWithId(long itemId, string barcode)
        {
            return itemService.StateChangeWithId(itemId, barcode);
        }
    }
}