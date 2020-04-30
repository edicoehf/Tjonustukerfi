using System;

namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>Data transfer object for item time stamp</summary>
    public class ItemTimeStampDTO
    {
        public long ItemId { get; set; }
        public long StateId { get; set; }
        public string State { get; set; }
        public DateTime TimeOfChange { get; set; }
    }
}