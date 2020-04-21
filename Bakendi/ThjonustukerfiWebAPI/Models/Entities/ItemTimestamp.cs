using System;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    /// <summary>Timestamps for state transition of items</summary>
    public class ItemTimestamp
    {
        public long Id { get; set; }
        public long ItemId { get; set; }
        public long StateId { get; set; }
        public DateTime TimeOfChange { get; set; }
    }
}