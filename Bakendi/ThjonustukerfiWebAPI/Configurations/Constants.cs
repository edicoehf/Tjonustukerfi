using System.Collections.Generic;

namespace ThjonustukerfiWebAPI.Configurations
{
    public static class Constants
    {
        public static string DBConnection { get; set; }
        public static bool sendEmail { get; set; }
        public static bool sendSMS { get; set; }
        public static List<string> Locations { get; set; }
    }
}