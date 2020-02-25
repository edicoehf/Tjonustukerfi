namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string JSON { get; set; }
    }
}