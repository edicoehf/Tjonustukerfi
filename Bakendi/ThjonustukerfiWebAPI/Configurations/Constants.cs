using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Configurations
{
    public class BarcodeImageDimensions
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
    public static class Constants
    {
        public static string DBConnection { get; set; }
        public static bool sendEmail { get; set; }
        public static bool sendSMS { get; set; }
        public static List<string> Locations { get; set; }
        public static BarcodeImageDimensions BarcodeImageSize { get; set; }
    }
}