using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using HandtolvuApp.Models.Json;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Polly;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.Data.Implementations
{
    public class ItemService : IItemService
    {
        readonly HttpClient _client;
        public NextStates NextStates { get; private set; }
        public Item Item { get; private set; }
        private readonly string BaseURI = $"{Constants.ApiConnection}items";
        private readonly string InfoURI = $"{Constants.ApiConnection}info";

        public ItemService()
        {
            _client = new HttpClient();
        }

        public async Task<Item> GetItemAsync(string barcode)
        {
            //set Item to null for future use
            Item = null;
            // String for Api call, might want to change this to constant
            string Uri = BaseURI + $"/search?barcode={barcode}";
            try
            {
                var response = await Policy
                                    .Handle<HttpRequestException>()
                                    .WaitAndRetry(retryCount: 3,
                                                    sleepDurationProvider: (attempt) => TimeSpan.FromSeconds(2))
                                    .Execute(async () => await _client.GetAsync(Uri));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Item = JsonConvert.DeserializeObject<Item>(content);
                    Item.ItemJson = JsonConvert.DeserializeObject<ItemJson>(Item.Json);
                    if (Item.Category == "Annað")
                    {
                        Item.Category = Item.ItemJson.OtherCategory;
                    }
                    if (Item.Service == "Annað")
                    {
                        Item.Service = Item.ItemJson.OtherService;
                    }
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
            NextStates = null;

            string Uri = BaseURI + $"/nextstate?barcode={barcode}";

            try
            {
                var response = await Policy
                                    .Handle<HttpRequestException>()
                                    .WaitAndRetry(retryCount: 3,
                                                    sleepDurationProvider: (attempt) => TimeSpan.FromSeconds(2))
                                    .Execute(async () => await _client.GetAsync(Uri));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    NextStates = JsonConvert.DeserializeObject<NextStates>(content);
                    NextStates.NextAvailableStates.Reverse();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"\tError {0}", ex.Message);
            }

            return NextStates;
        }

        public async Task<List<LocationStateChange>> StateChangeByLocation(List<LocationStateChange> items)
        {
            List<LocationStateChange> ret = new List<LocationStateChange>();
            
            string stateUri = BaseURI + "/scanner/statechangebybarcode";

            try
            {
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, stateUri)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json")
                };
                var response = await Policy
                                    .Handle<HttpRequestException>()
                                    .WaitAndRetry(retryCount: 3,
                                                    sleepDurationProvider: (attempt) => TimeSpan.FromSeconds(2))
                                    .Execute(async () => await _client.SendAsync(request));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if(content != "")
                    {
                        ret = JsonConvert.DeserializeObject<List<LocationStateChange>>(content);
                    }
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(@"\tError {0}", ex.Message);
            }

            return ret;
        }

        public async Task<List<string>> GetAllLocations()
        {
            string reqUri = InfoURI + "/itemlocations";
            List<string> ret = new List<string>();

            try
            {
                var response = await Policy
                                    .Handle<HttpRequestException>()
                                    .WaitAndRetry(retryCount: 3,
                                                    sleepDurationProvider: (attempt) => TimeSpan.FromSeconds(2))
                                    .Execute(async () => await _client.GetAsync(reqUri));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ret = JsonConvert.DeserializeObject<List<string>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tError {0}", ex.Message);
            }          

            return ret;
        }

        public async Task<List<State>> GetAllStates()
        {
            string reqUri = InfoURI + "/states";
            List<State> ret = new List<State>();
           
            try
            {
                var response = await Policy
                                    .Handle<HttpRequestException>()
                                    .WaitAndRetry(retryCount: 3,
                                                    sleepDurationProvider: (attempt) => TimeSpan.FromSeconds(2))
                                    .Execute(async () => await _client.GetAsync(reqUri));

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ret = JsonConvert.DeserializeObject<List<State>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tError {0}", ex.Message);
            }  

            return ret;
        }
    }
}
