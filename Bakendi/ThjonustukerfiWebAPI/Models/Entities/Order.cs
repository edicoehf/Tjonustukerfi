using System.ComponentModel.DataAnnotations.Schema;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class Order
    {
        public long Id { get; set; }
        [ForeignKey("Customer")]
        public long CustomerId { get; set; }
        public string Barcode { get; set; }
        public string JSON { get; set; }
    }
}