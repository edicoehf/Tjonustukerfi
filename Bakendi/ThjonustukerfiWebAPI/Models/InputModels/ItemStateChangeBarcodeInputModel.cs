namespace ThjonustukerfiWebAPI.Models.InputModels
{
    public class ItemStateChangeBarcodeInputModel
    {
        public string Barcode { get; set; }
        public long StateChangeTo { get; set; }

        //*     Overrides     *//
        public static bool operator ==(ItemStateChangeBarcodeInputModel i1, ItemStateChangeBarcodeInputModel i2)
        {
            if(object.ReferenceEquals(i1, i2))
            {
                return true;
            }
            if(object.ReferenceEquals(i1, null) || object.ReferenceEquals(i2, null))
            {
                return false;
            }

            return i1.Barcode == i2.Barcode && i1.StateChangeTo == i2.StateChangeTo;
        }

        public static bool operator !=(ItemStateChangeBarcodeInputModel i1, ItemStateChangeBarcodeInputModel i2)
        {
            return !(i1 == i2);
        }

        public override int GetHashCode()
        {
            int bc = 0;
            foreach (char c in Barcode)
            {
                bc += c.GetHashCode();
            }
            bc += StateChangeTo.GetHashCode();
            
            return bc;
        }

        public bool Equals(ItemStateChangeBarcodeInputModel other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemStateChangeBarcodeInputModel);
        }
    }
}