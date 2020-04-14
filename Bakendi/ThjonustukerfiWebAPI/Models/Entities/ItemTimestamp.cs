using System;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class ItemTimestamp
    {
        public long Id { get; set; }    // same as the item connected to it
        public long ItemId { get; set; }
        public long StateId { get; set; }
        public DateTime TimeOfChange { get; set; }
    }
}