using System.Collections.Generic;
using ThjonustukerfiWebAPI.Models.Entities;

namespace ThjonustukerfiWebAPI.Config
{
    /// <summary>Settings for the barcode image, read from company setup file</summary>
    public class BarcodeImage
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }
    
    /// <summary>Representation of the company setup json file. Used to simplify the setup process.</summary>
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
        public string CompanyEmail { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCc { get; set; }
        public string CompanyEmailDisclaimer { get; set; }
    }
}