using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.Data.Implementations
{
    public class ItemService : IItemService
    {
        HttpClient _client;

        public NextStates nextStates { get; private set; }

        public Item Item { get; private set; }

        public ItemService()
        {
            _client = new HttpClient();
        }

        public async Task<Item> GetItemAsync(string barcode)
        {
            //set Item to null for future use
            Item = null;
            // String for Api call, might want to change this to constant
            string Uri = "http://10.0.2.2:5000/api/items/search?barcode=" + barcode;
            try
            {
                var response = await _client.GetAsync(Uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Item = JsonConvert.DeserializeObject<Item>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Item;
        }

        public async Task<NextStates> GetNextStatesAsync(string barcode)
        {
            nextStates = null;

            string Uri = $"http://10.0.2.2:5000/api/items/nextstate?barcode={barcode}";

            try
            {
                var response = await _client.GetAsync(Uri);
                
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    nextStates = JsonConvert.DeserializeObject<NextStates>(content);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"\tError {0}", ex.Message);
            }

            return nextStates;
        }

        public async Task StateChangeWithId(long id, string barcode)
        {
            string stateUri = "http://10.0.2.2:5000/api/items/statechangebyid";
            var item = new[] { new { itemId = id, stateChangeBarcode = barcode } };

            try
            {
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, stateUri) { 
                    Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json") 
                };
                var response = await _client.SendAsync(request);

                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tOrder successfully completed");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
