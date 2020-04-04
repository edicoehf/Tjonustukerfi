using System;

namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>Data transfer object for Item entity, shows the state information of the item.</summary>
    public class ItemStateDTO
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public DateTime DateModified { get; set; }

        //*     Overrides     *//
        public static bool operator ==(ItemStateDTO is1, ItemStateDTO is2)
        {
            if(object.ReferenceEquals(is1, is2)) { return true; }

            if(object.ReferenceEquals(is1, null) || object.ReferenceEquals(is2, null)) { return false; }

            return is1.Id == is2.Id && is1.OrderId == is2.OrderId && is1.Type == is2.Type && is1.State == is2.State && is1.DateModified == is2.DateModified;
        }

        public static bool operator !=(ItemStateDTO is1, ItemStateDTO is2)
        {
            return !(is1 == is2);
        }

        public override int GetHashCode()
        {
            return (int) Id;
        }

        public bool Equals(ItemStateDTO other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemStateDTO);
        }
    }
}