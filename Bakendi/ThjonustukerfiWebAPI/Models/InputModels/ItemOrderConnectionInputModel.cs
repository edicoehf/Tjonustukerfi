using System.ComponentModel.DataAnnotations;

namespace ThjonustukerfiWebAPI.Models.InputModels
{
    public class ItemOrderConnectionInputModel
    {
        [Required]
        public long OrderId { get; set; }
        public long ItemId { get; set; }
    }
}