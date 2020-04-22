using System.ComponentModel.DataAnnotations;

namespace ThjonustukerfiWebAPI.Models.InputModels
{
    /// <summary>Input model used to change the state of an item by barcode.</summary>
    public class ItemStateChangeBarcodeScanner
    {
        [Required]
        public string ItemBarcode { get; set; }
        [Required]
        public string StateChangeBarcode { get; set; }

        //*     Overrides     *//
        public static bool operator ==(ItemStateChangeBarcodeScanner i1, ItemStateChangeBarcodeScanner i2)
        {
            if(object.ReferenceEquals(i1, i2))
            {
                return true;
            }
            if(object.ReferenceEquals(i1, null) || object.ReferenceEquals(i2, null))
            {
                return false;
            }

            return i1.ItemBarcode == i2.ItemBarcode && i1.StateChangeBarcode == i2.StateChangeBarcode;
        }

        public static bool operator !=(ItemStateChangeBarcodeScanner i1, ItemStateChangeBarcodeScanner i2)
        {
            return !(i1 == i2);
        }

        public override int GetHashCode()
        {
            int bc = 0;
            foreach (char c in ItemBarcode)
            {
                bc += c.GetHashCode();
            }
            foreach (char c in StateChangeBarcode)
            {
                bc += c.GetHashCode();
            }
            
            return bc;
        }

        public bool Equals(ItemStateChangeBarcodeScanner other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemStateChangeBarcodeScanner);
        }
    }
}