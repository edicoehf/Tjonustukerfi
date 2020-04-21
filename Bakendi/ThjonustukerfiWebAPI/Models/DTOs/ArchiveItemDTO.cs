using System;

namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>DTO that represents an archived Item</summary>
    public class ArchiveItemDTO
    {
        public string Category { get; set; }
        public long? CategoryId { get; set; }
        public string Service { get; set; }
        public long? ServiceId { get; set; }
        public string extraDataJSON { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}