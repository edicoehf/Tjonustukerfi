﻿using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
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
        private readonly string BaseURI = $"{Constants.ApiConnection}/api/items";
        private readonly string InfoURI = $"{Constants.ApiConnection}/api/info";

        public ItemService()
        {
            _client = new HttpClient();
        }

        public async Task<Item> GetItemAsync(string barcode)
        {
            if(CheckConnection())
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
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"\tERROR {0}", ex.Message);
                }
            }
            else
            {
                // Handle connection issues
            }

            return Item;
        }

        public async Task<NextStates> GetNextStatesAsync(string barcode)
        {
            if(CheckConnection())
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
            }
            else
            {
                // Handle Connection issues
            }

            return NextStates;
        }

        public async Task<bool> StateChangeWithId(long id, string barcode)
        {
            if(CheckConnection())
            {
                string stateUri = BaseURI + "/scanner/statechangebyid";
                var item = new[] { new { itemId = id, stateChangeBarcode = barcode } };

                try
                {
                    var method = new HttpMethod("PATCH");
                    var request = new HttpRequestMessage(method, stateUri) { 
                        Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json") 
                    };
                    var response = await Policy
                                        .Handle<HttpRequestException>()
                                        .WaitAndRetry(retryCount: 3,
                                                        sleepDurationProvider: (attempt) => TimeSpan.FromSeconds(2))
                                        .Execute(async () => await _client.SendAsync(request));

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"\tERROR {0}", ex.Message);
                    return false;
                }
            }
            else
            {
                // handle connection issues
            }

            return false;
        }

        public async Task<List<LocationStateChange>> StateChangeByLocation(ObservableCollection<string> items, string barcode)
        {
            List<LocationStateChange> ret = new List<LocationStateChange>();
            
            if(CheckConnection())
            {
                string stateUri = BaseURI + "/scanner/statechangebybarcode";
                var item = new List<LocationStateChange>();
                foreach(string i in items)
                {
                    item.Add(new LocationStateChange { ItemBarcode = i, StateChangeBarcode = barcode });
                }

                try
                {
                    var method = new HttpMethod("PATCH");
                    var request = new HttpRequestMessage(method, stateUri)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json")
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

            }
            else
            {
                // Handle connection issues
            }
            return ret;
        }

        public async Task<List<string>> GetAllLocations()
        {
            string reqUri = InfoURI + "/itemlocations";
            List<string> ret = new List<string>();

            if(CheckConnection())
            {
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
            }
            else
            {
                // Handle Connection issues
            }

            return ret;
        }

        public async Task<List<State>> GetAllStates()
        {
            string reqUri = InfoURI + "/states";
            List<State> ret = new List<State>();
            if(CheckConnection())
            {
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
            }
            else
            {
                // Handle connection issues
            }

            return ret;
        }

        private bool CheckConnection()
        {
            return CrossConnectivity.Current.IsConnected;
        }

    }
}
