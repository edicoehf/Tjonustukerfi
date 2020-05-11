using HandtolvuApp.Models;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HandtolvuApp.FailRequestHandler
{
    public class FailedRequestManager
    {
        public async Task SendFailedItemRequests()
        {
            FailedRequstCollection.ItemFailedRequests = await App.ItemManager.StateChangeByLocation(FailedRequstCollection.ItemFailedRequests);
        }
    }
}
