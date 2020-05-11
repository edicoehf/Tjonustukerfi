using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.FailRequestHandler
{
    public static class FailedRequstCollection
    {
        public static List<LocationStateChange> ItemFailedRequests { get; set; } = new List<LocationStateChange>();
    }
}
