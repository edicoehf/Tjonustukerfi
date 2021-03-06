﻿using HandtolvuApp.Models.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HandtolvuApp.Models
{
    /// <summary>
    ///     Class to represent single item gotten from API
    /// </summary>
    public class Item
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string Service { get; set; }
        public string Category { get; set; }
        public string State { get; set; }
        public string Barcode { get; set; }
        public string Json { get; set; }
        public ItemJson ItemJson { get; set; }
        public DateTime DateModified { get; set; }
    }
}
