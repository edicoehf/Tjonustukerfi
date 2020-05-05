using System;
using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>Data transfer object for Order entity, provides detailed information of Order.</summary>
    public class OrderDTO
    {
        public long Id { get; set; }
        public string Customer { get; set; }
        public long CustomerId { get; set; }
        public string Barcode { get; set; }
        public string CustomerEmail { get; set; }
        public List<ItemDTO> Items { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}