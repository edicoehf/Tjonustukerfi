using System.ComponentModel.DataAnnotations;

namespace ThjonustukerfiWebAPI.Models.InputModels
{
    /// <summary>Input model used to change the state of an item by ID.</summary>
    public class ItemStateChangeInputIdScanner
    {
        [Required]
        public long? ItemId { get; set; }
        [Required]
        public string StateChangeBarcode { get; set; }

        //*     Overrides     *//
        public static bool operator ==(ItemStateChangeInputIdScanner i1, ItemStateChangeInputIdScanner i2)
        {
            if(object.ReferenceEquals(i1, i2))
            {
                return true;
            }
            if(object.ReferenceEquals(i1, null) || object.ReferenceEquals(i2, null))
            {
                return false;
            }

            return i1.StateChangeBarcode == i2.StateChangeBarcode && i1.ItemId == i2.ItemId;
        }

        public static bool operator !=(ItemStateChangeInputIdScanner i1, ItemStateChangeInputIdScanner i2)
        {
            return !(i1 == i2);
        }

        public override int GetHashCode()
        {
            int bc = 0;
            foreach (char c in StateChangeBarcode)
            {
                bc += c.GetHashCode();
            }
            bc += ItemId.GetHashCode();
            
            return bc;
        }

        public bool Equals(ItemStateChangeInputIdScanner other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemStateChangeInputIdScanner);
        }
    }
}