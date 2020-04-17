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
        public Order Order { get; private set; }

        public RestService()
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
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Item = JsonConvert.DeserializeObject<Item>(content);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Item;
        }

        public async Task<Order> GetOrderAsync(string barcode)
        {
            Order = null;

            string Uri = "http://10.0.2.2:5000/api/orders/search?barcode=" + barcode;
            try
            {
                var response = await _client.GetAsync(Uri);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Order = JsonConvert.DeserializeObject<Order>(content);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Order;
        }

        public async Task CheckoutOrder(long id)
        {
            // uri to set order to completed state
            string checkoutUri = "http://10.0.2.2:5000/api/orders/";
            try
            {
                var method = new HttpMethod("PATCH");
                checkoutUri = checkoutUri + id + "/complete";

                var request = new HttpRequestMessage(method, checkoutUri);
                var response = await _client.SendAsync(request);
                
                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tOrder successfully completed");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\rERROR {0}", ex.Message);
            }
        }
    }
}
