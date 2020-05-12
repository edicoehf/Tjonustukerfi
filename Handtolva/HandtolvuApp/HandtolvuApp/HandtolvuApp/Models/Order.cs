using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    /// <summary>
    ///     Model for orders gotten from API
    ///     
    ///     Allso holds list for all items in the order
    /// </summary>
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
