using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.FailRequestHandler
{
    /// <summary>
    ///     Lists of all failed requests requests due to lack of internet connection
    /// </summary>
    public static class FailedRequstCollection
    {
        public static List<LocationStateChange> ItemFailedRequests { get; set; } = new List<LocationStateChange>();
    }
}
