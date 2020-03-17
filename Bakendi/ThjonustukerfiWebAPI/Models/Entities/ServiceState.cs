using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    public class ServiceState  : IEquatable<ServiceState>
    {
        public long Id { get; set; }
        [ForeignKey("Service")]
        public long ServiceId { get; set; } // tengist birkireyk
        [ForeignKey("State")]
        public long StateId { get; set; }   // tenging við state
        public int Step { get; set; }       // hvaða skref í ferlinu þetta er

        // Overrides
        public override int GetHashCode()
        {
            return (int) Id;
        }

        public static bool operator ==(ServiceState s1, ServiceState s2)
        {
            if(object.ReferenceEquals(s1, s2))
            {
                return true;
            }
            if(object.ReferenceEquals(s1, null) || object.ReferenceEquals(s2, null))
            {
                return false;
            }

            return  s1.Id == s2.Id && s1.ServiceId == s2.ServiceId
                    && s1.StateId == s2.StateId && s1.Step == s2.Step;
        }

        public static bool operator !=(ServiceState s1, ServiceState s2)
        {
            return !(s1 == s2);
        }

        public bool Equals(ServiceState other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ServiceState);
        }
    }
}