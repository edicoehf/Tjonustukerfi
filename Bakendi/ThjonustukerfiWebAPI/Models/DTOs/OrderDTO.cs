using System;
using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Models.DTOs
{
    public class OrderDTO
    {
        public long Id { get; set; }
        public string Customer { get; set; }
        public string Barcode { get; set; }
        public List<ItemDTO> Items { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}