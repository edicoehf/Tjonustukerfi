using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    public class Item
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public DateTime DateModified { get; set; }
    }
}
