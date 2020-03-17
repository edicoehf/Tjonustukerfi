using System.ComponentModel.DataAnnotations.Schema;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class ServiceState
    {
        public long Id { get; set; }
        [ForeignKey("Service")]
        public long ServiceId { get; set; } // tengist birkireyk
        [ForeignKey("State")]
        public long StateId { get; set; }   // tenging við state
        public int Step { get; set; }       // hvaða skref í ferlinu þetta er
    }
}