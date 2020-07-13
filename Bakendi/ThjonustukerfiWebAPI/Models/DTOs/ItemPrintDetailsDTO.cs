namespace ThjonustukerfiWebAPI.Models.DTOs
{
    /// <summary>DTO that provides printer information</summary>
    public class ItemPrintDetailsDTO
    {
        public long   Id           { get; set; }
        public long   OrderId      { get; set; }
        public string Category     { get; set; }
        public string Service      { get; set; }
        public string Barcode      { get; set; }
        public string Details      { get; set; }
        public string JSON         { get; set; }
        public string BarcodeImage { get; set; }
        public string Quantity     { get; set; }
    }
}