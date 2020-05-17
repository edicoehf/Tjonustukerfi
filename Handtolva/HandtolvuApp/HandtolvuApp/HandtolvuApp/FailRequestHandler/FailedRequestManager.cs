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
        /// <summary>
        ///     Send StateChagne request for all failed request that failed due to lack of internet connection
        /// </summary>
        /// <returns></returns>
        public async Task SendFailedItemRequests()
        {
            FailedRequstCollection.ItemFailedRequests = await App.ItemManager.StateChangeByLocation(FailedRequstCollection.ItemFailedRequests);
        }
    }
}
