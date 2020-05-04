using System.Drawing;

namespace ThjonustukerfiWebAPI.Models.DTOs
{
    public class ItemPrintDetailsDTO
    {
        public string Category { get; set; }
        public string Service { get; set; }
        public string Barcode { get; set; }
        public string JSON { get; set; }
        public string BarcodeImage { get; set; }
    }
}