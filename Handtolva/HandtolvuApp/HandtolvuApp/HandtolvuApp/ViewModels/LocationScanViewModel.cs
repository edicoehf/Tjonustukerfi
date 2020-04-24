using HandtolvuApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class LocationScanViewModel : ScannerViewModel
    {

        public LocationScanViewModel(IScannerService scannerService) : base(scannerService) 
        {
            Placeholder = "Sláðu inn vörunúmer";

            ScannedBarcodeText = "";

            ClickCommand = new Command(async () =>
            {
                // handle scan location
            });
        }

        public Command ClickCommand { get; }
    }
}
