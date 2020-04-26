using System;
using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>DTO that represents an archived Order</summary>
    public class ArchiveOrderDTO
    {
        public long Id { get; set; }
        public string Customer { get; set; }
        public long? CustomerId { get; set; }
        public string JSON { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }
        public int OrderSize { get; set; }
        public List<ArchiveItemDTO> Items { get; set; }
    }
}