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

        public ItemService()
        {
            _client = new HttpClient();
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
    }
}
