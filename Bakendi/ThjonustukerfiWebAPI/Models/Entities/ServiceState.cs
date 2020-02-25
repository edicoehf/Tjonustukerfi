using System.ComponentModel.DataAnnotations.Schema;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class ServiceState
    {
        [ForeignKey("Service")]
        public long ServiceId { get; set; }
        [ForeignKey("State")]
        public long StateId { get; set; }
        public int Step { get; set; }
    }
}