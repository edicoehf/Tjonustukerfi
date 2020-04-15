using HandtolvuApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.Data
{
    public class RestService : IRestService
    {
        HttpClient _client;

        public Item Item { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<Item> GetItemAsync(string barcode)
        {
            //set Item to null for future use
            Item = null;
            // String for Api call, might want to change this to constant
            string Uri = "http://10.0.2.2:5000/api/items?search=" + barcode;
            try
            {
                var response = await _client.GetAsync(Uri);
                if(response.IsSuccessStatusCode)
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
    }
}
