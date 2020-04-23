using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.Entities;

namespace ThjonustukerfiWebAPI.Models.InputModels
{
    /// <summary>Used for input in Archive. Used to hold the correct items with the correct order</summary>
    public class OrderItemArchiveInput
    {
        public Order Order { get; set; }
        public OrderArchive ArchivedOrder { get; set; }
        public List<Item> Items { get; set; }
        public List<ItemArchive> ArchivedItems { get; set; }
    }
}