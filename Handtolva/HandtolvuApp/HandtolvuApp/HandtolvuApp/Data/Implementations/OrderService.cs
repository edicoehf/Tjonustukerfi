﻿using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using HandtolvuApp.Models.Json;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HandtolvuApp.Data.Implementations
{
    public class OrderService : IOrderService
    {
        readonly HttpClient _client;
        private static readonly string BaseURI = $"{Constants.ApiConnection}orders";
        public Order Order { get; private set; }

        public OrderService()
        {
            _client = new HttpClient();
        }

        public async Task<Order> GetOrderAsync(string barcode)
        {
            Order = null;
            
            string Uri = BaseURI + $"/search?barcode={barcode}";
            try
            {
                // Get response with retry policy in case of HttpRequestException
                var response = await Policy
                                    .Handle<HttpRequestException>()
                                    .WaitAndRetry(retryCount: 3,
                                                    sleepDurationProvider: (attempt) => TimeSpan.FromSeconds(2))
                                    .Execute(async () => await _client.GetAsync(Uri));

                if (response.IsSuccessStatusCode)
                {
                    // Convert response content to Order
                    var content = await response.Content.ReadAsStringAsync();
                    Order = JsonConvert.DeserializeObject<Order>(content);
                    // For each item in order convert additional information to ItemJson and replace places With "Annað"
                    foreach(var i in Order.Items)
                    {
                        i.ItemJson = JsonConvert.DeserializeObject<ItemJson>(i.Json);
                        if(i.Category == "Annað")
                        {
                            i.Category = i.ItemJson.OtherCategory;
                        }
                        if(i.Service == "Annað")
                        {
                            i.Service = i.ItemJson.OtherService;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            } 

            return Order;
        }

        public async Task<bool> CheckoutOrder(long id)
        {
            // uri to set order to completed state
            try
            {
                var method = new HttpMethod("PATCH");
                string checkoutUri = BaseURI + $"/{id}/complete";

                var request = new HttpRequestMessage(method, checkoutUri);

                // Get response with retry policy in case of HttpRequestException
                var response = await Policy
                                    .Handle<HttpRequestException>()
                                    .WaitAndRetry(retryCount: 3,
                                                    sleepDurationProvider: (attempt) => TimeSpan.FromSeconds(2))
                                    .Execute(async () => await _client.SendAsync(request));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\rERROR {0}", ex.Message);
            }
            
            return false;
        }
    }
}
