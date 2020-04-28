using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    public class Item
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string Service { get; set; }
        public string Category { get; set; }
        public string State { get; set; }
        public string Barcode { get; set; }
        public string Json { get; set; }
        public DateTime DateModified { get; set; }
    }
}
