using System.ComponentModel.DataAnnotations.Schema;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class ItemOrderConnection
    {
        [ForeignKey("Item")]
        public long ItemId { get; set; }
        [ForeignKey("Oder")]
        public long OrderId { get; set; }
    }
}