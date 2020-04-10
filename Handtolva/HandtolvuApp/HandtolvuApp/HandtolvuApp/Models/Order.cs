using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    public class Order
    {
        public long Id { get; set; }
        public string Customer { get; set; }
        public long CustomerId { get; set; }
        public string Barcode { get; set; }
        public List<Item> Items { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
