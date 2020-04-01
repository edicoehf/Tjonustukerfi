using System;

namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>Data transfer object that shows the state of the item</summary>
    public class ItemStateDTO
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public DateTime DateModified { get; set; }
    }
}