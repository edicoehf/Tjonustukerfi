using System.ComponentModel.DataAnnotations;

namespace ThjonustukerfiWebAPI.Models.InputModels
{
    public class ItemStateChangeInput
    {
        [Required]
        public long? ItemId { get; set; }
        [Required]
        public long? StateChangeTo { get; set; }
        public string Location { get; set; }

        //*     Overrides     *//
        public static bool operator ==(ItemStateChangeInput i1, ItemStateChangeInput i2)
        {
            if(object.ReferenceEquals(i1, i2))
            {
                return true;
            }
            if(object.ReferenceEquals(i1, null) || object.ReferenceEquals(i2, null))
            {
                return false;
            }

            return i1.ItemId == i2.ItemId && i1.StateChangeTo == i2.StateChangeTo && i1.Location == i2.Location;
        }

        public static bool operator !=(ItemStateChangeInput i1, ItemStateChangeInput i2)
        {
            return !(i1 == i2);
        }

        public override int GetHashCode()
        {
            int bc = 0;
            
            bc += StateChangeTo.GetHashCode();
            bc += ItemId.GetHashCode();
            
            return bc;
        }

        public bool Equals(ItemStateChangeInput other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemStateChangeInput);
        }
    }
}