using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    /// <summary>Representation of the Order entity stored in the database.</summary>
    public class Order
    {
        public long Id { get; set; }
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public string Barcode { get; set; }
        public string JSON { get; set; }
        // Auto generated
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime? DateCompleted { get; set; }

    }
}