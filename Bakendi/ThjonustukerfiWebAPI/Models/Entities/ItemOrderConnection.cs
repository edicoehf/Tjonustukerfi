using System.ComponentModel.DataAnnotations.Schema;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    /// <summary>Representation of the Item-Order-Connections entity stored in the database.</summary>
    public class ItemOrderConnection
    {
        public long Id { get; set; }
        [ForeignKey("Item")]
        public long ItemId { get; set; }
        [ForeignKey("Order")]
        public long OrderId { get; set; }
    }
}