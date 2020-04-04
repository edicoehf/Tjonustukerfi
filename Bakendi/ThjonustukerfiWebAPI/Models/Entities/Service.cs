using System;

namespace ThjonustukerfiWebAPI.Models.Entities
{
    /// <summary>Representation of the Service entity stored in the database.</summary>
    public class Service : IEquatable<Service>
    {
        public long Id { get; set; }
        public string Name { get; set; }    // E.g. Birkireyking
        public string JSON { get; set; }

        // Overrides
        public override int GetHashCode()
        {
            return (int) Id;
        }

        public static bool operator ==(Service s1, Service s2)
        {
            if(object.ReferenceEquals(s1, s2))
            {
                return true;
            }
            if(object.ReferenceEquals(s1, null) || object.ReferenceEquals(s2, null))
            {
                return false;
            }

            return s1.Id == s2.Id && s1.Name == s2.Name && s1.JSON == s2.JSON;
        }

        public static bool operator !=(Service s1, Service s2)
        {
            return !(s1 == s2);
        }

        public bool Equals(Service other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Service);
        }
    }
}