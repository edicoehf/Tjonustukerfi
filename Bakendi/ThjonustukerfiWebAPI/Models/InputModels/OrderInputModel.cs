using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Models.InputModels
{
    /// <summary>Input model to add new Order to the database.</summary>
    public class OrderInputModel
    {
        [Required]
        public long CustomerId { get; set; }
        public List<ItemInputModel> Items { get; set; }
    }
}