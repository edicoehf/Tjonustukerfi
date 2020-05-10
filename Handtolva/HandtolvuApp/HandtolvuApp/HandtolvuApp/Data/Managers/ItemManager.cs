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

        /// <summary>Gets an item based on barcode </summary>
        /// <param name="barcode">Barcode of a given item</param>
        /// <returns> Returns the Item model </returns>
        public Task<Item> GetItemAsync(string barcode)
        {
            return itemService.GetItemAsync(barcode);
        }

        /// <summary> Gets all next possible states for item</summary>
        /// <param name="barcode">Barcode of the item</param>
        /// <returns>returns NextStates model</returns>
        public Task<NextStates> GetNextStatesAsync(string barcode)
        {
            return itemService.GetNextStatesAsync(barcode);
        }

        /// <summary>Changes state&location of the item</summary>
        /// <param name="itemId">Id of the item</param>
        /// <param name="barcode">Barcode of the location</param>
        /// <returns>Returns true for success and false for fail</returns>
        public Task<bool> StateChangeWithId(long itemId, string barcode)
        {
            return itemService.StateChangeWithId(itemId, barcode);
        }

        /// <summary>Updates a list of items to the same state/location</summary>
        /// <param name="items">List of items to change states</param>
        /// <param name="barcode">Barcode of the state/location</param>
        /// <returns>Returns a list of itms for all unsuccessful state/location changes</returns>
        public Task<List<LocationStateChange>> StateChangeByLocation(ObservableCollection<string> items, string barcode)
        {
            return itemService.StateChangeByLocation(items, barcode);
        }

        /// <summary>Gets all locations</summary>
        /// <returns>List of strings that are locations</returns>
        public Task<List<string>> GetAllLoctions()
        {
            return itemService.GetAllLocations();
        }

        /// <summary>Gets all states</summary>
        /// <returns>Returns a list of State model</returns>
        public Task<List<State>> GetAllStates()
        {
            return itemService.GetAllStates();
        }
    }
}
