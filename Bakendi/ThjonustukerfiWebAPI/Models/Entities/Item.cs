using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class Item
    {
        public long Id { get; set; }
        public string Type { get; set; }
        [ForeignKey("State")]
        public long StateId { get; set; }
        [ForeignKey("Service")]
        public long ServiceId { get; set; }
        public string Barcode { get; set; }
        public string JSON { get; set; }    // t.d. bitar, heil flök o.s.frv.
        // Auto generated
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}