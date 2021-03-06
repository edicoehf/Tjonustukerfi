using System;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    /// <summary>Entity for archiving orders</summary>
    public class OrderArchive
    {
        public long Id { get; set; }
        public string Customer { get; set; }
        public string CustomerEmail { get; set; }
        public string JSON { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}