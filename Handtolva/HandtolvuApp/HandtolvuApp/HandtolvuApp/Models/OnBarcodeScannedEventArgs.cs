using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Models
{
    public class OnBarcodeScannedEventArgs
    {
        public IEnumerable<ScannedBarcodes> Barcodes { get; set; }

        public OnBarcodeScannedEventArgs(IEnumerable<ScannedBarcodes> barcodes)
        {
            Barcodes = barcodes;
        }
    }
}
