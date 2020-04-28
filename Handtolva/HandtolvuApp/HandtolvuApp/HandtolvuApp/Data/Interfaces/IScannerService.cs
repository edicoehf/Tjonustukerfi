using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandtolvuApp.Data.Interfaces
{
    public interface IScannerService
    {
        event EventHandler<OnBarcodeScannedEventArgs> OnBarcodeScanned;
    }
}
