using System.ComponentModel.DataAnnotations;

namespace ThjonustukerfiWebAPI.Models.InputModels
{
    /// <summary>Input model to add new Item to the database.</summary>
    public class ItemInputModel
    {
        [Required]
        public long? CategoryId { get; set; }
        [Required]
        public long? ServiceId { get; set; }

        [Required]
        public bool? Sliced { get; set; }
        [Required]
        public bool? Filleted { get; set; }
        public string OtherCategory { get; set; }
        public string OtherService { get; set; }
        public string Details { get; set; }

        //*     Overrides     *//
        public static bool operator ==(ItemInputModel i1, ItemInputModel i2)
        {
            if(object.ReferenceEquals(i1, i2))
            {
                return true;
            }
            if(object.ReferenceEquals(i1, null) || object.ReferenceEquals(i2, null))
            {
                return false;
            }

            return i1.CategoryId == i2.CategoryId && i1.ServiceId == i2.ServiceId;
        }

        public static bool operator !=(ItemInputModel i1, ItemInputModel i2)
        {
            return !(i1 == i2);
        }

        public override int GetHashCode()
        {
            int bc = 0;

            bc += CategoryId.GetHashCode();
            bc += ServiceId.GetHashCode();
            
            return bc;
        }

        public bool Equals(ItemInputModel other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemInputModel);
        }
    }
}