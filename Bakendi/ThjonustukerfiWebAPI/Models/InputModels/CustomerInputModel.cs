using System.ComponentModel.DataAnnotations;

namespace ThjonustukerfiWebAPI.Models.InputModels
{
    // TODO regex for phone
    // TODO regex for postalcode
    public class CustomerInputModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [RegularExpression(@"^(0?[1-9]|[12][0-9]|3[01])(1[0-2]|0?[1-9])[0-9]{2}(-?)([0-9]{3})[890]$")]
        public string SSN { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }
}