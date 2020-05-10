namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>Data transfer object for customer entity, provides detailed information of customer.</summary>
    public class CustomerDetailsDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public bool HasReadyOrders { get; set; }
        public string JSON { get; set; }
    }
}