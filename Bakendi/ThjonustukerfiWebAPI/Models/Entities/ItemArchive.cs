using System;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    /// <summary>Entity for archiving items</summary>
    public class ItemArchive
    {
        public long Id { get; set; }
        public long OrderArchiveId { get; set; }    // The id of the connected order in order archive
        public string Category { get; set; }
        public long? CategoryId { get; set; }
        public string Service { get; set; }
        public long? ServiceId { get; set; }
        public string extraDataJSON { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }
    }
}