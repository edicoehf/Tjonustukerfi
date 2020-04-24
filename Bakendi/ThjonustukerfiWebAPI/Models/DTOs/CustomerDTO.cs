namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>Data transfer object for customer entity, provides basic information of customer.</summary>
    public class CustomerDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
		public string Email { get; set; }
    }
}