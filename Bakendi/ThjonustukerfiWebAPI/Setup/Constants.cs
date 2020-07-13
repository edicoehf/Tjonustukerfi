using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Setup
{
    /// <summary>Barcode dimensions that are sent via email</summary>
    public static class BarcodeImageDimensions
    {
        private static int _defaultValue = 100;
        public static int Width { get; set; } = _defaultValue;
        public static int Height { get; set; } = _defaultValue;
    }

    /// <summary>Constants that the application always needs</summary>
    public static class Constants
    {
        public static string DBConnection { get; set; }
        public static bool sendEmail { get; set; }
        public static bool sendSMS { get; set; }
        public static List<string> Locations { get; set; }
        public static string CompanyEmail { get; set; }
        public static string CompanyCc {get; set;}
        public static string CompanyEmailDisclaimer {get; set;}
        public static string CompanyName { get; set; }
    }
}