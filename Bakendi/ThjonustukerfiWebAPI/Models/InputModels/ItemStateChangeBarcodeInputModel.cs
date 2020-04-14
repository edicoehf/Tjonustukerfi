namespace ThjonustukerfiWebAPI.Models.InputModels
{
    public class ItemStateChangeBarcodeInputModel
    {
        public string Barcode { get; set; }
        public long StateChangeTo { get; set; }
    }
}