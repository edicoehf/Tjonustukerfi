using System;

namespace ThjonustukerfiWebAPI.Models.InputModels
{
    /// <summary>Used to insert timestamp to item archive</summary>
    public class TimestampArchiveInput
    {
        public long ItemId { get; set; }
        public long StateId { get; set; }
        public DateTime TimeOfChange { get; set; }
    }
}