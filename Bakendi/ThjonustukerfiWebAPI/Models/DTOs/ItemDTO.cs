namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>Data transfer object for Item entity, provides basic information of Item.</summary>
    public class ItemDTO
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Service { get; set; }
        public string Barcode { get; set; }

        //*     Overrides     *//
        public static bool operator ==(ItemDTO i1, ItemDTO i2)
        {
            if(object.ReferenceEquals(i1, i2))
            {
                return true;
            }
            if(object.ReferenceEquals(i1, null) || object.ReferenceEquals(i2, null))
            {
                return false;
            }

            return i1.Id == i2.Id && i1.Type == i2.Type && i1.Service == i2.Service && i1.Barcode == i2.Barcode;
        }

        public static bool operator !=(ItemDTO i1, ItemDTO i2)
        {
            return !(i1 == i2);
        }

        public override int GetHashCode()
        {
            return (int) Id;
        }

        public bool Equals(ItemDTO other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemDTO);
        }
    }
}