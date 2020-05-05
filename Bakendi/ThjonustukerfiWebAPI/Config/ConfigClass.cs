using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.Entities;

namespace ThjonustukerfiWebAPI.Config
{
    public class BarcodeImage
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }
    public class ConfigClass
    {
        public List<string> Locations { get; set; }
        public BarcodeImage BarcodeImageDimensions { get; set; }
        public List<Service> Services { get; set; }
        public List<ServiceState> ServiceStates { get; set; }
        public List<State> States { get; set; }
        public List<Category> Categories { get; set; }
        public bool SendSMS { get; set; }
        public bool SendEmail { get; set; }
        public int FinalStep { get; set; }
        public int SendNotificationStep { get; set; }
    }
}