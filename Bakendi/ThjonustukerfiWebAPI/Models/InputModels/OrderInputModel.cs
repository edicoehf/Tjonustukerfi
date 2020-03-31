using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Models.InputModels
{
    public class OrderInputModel
    {
        [Required]
        public long CustomerId { get; set; }
        public List<ItemInputModel> Items { get; set; }
    }
}