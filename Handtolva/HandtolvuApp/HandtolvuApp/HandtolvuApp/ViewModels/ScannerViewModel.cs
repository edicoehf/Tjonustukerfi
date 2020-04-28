using HandtolvuApp.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandtolvuApp.ViewModels
{
    public class ScannerViewModel : BaseViewModel
    {
        private string _scannedBarcodeText;

        public string ScannedBarcodeText
        {
            get { return _scannedBarcodeText; }
            set
            {
                _scannedBarcodeText = value;
                NotifyPropertyChanged(nameof(ScannedBarcodeText));
            }
        }

        string _placeholder;

        public string Placeholder
        {
            get => _placeholder;

            set
            {
                _placeholder = value;

                NotifyPropertyChanged(nameof(Placeholder));
            }
        }

        public IScannerService _scannerService;

        public ScannerViewModel()
        {
            if(App.Scanner != null)
            {
                _scannerService = App.Scanner;
                _scannerService.OnBarcodeScanned += _scannerService_OnBarcodeScanned;
            }
        }

        void _scannerService_OnBarcodeScanned(object sender, Models.OnBarcodeScannedEventArgs e)
        {
            var scannedBarcode = e?.Barcodes?.FirstOrDefault();
            if(scannedBarcode == null)
            {
                ScannedBarcodeText = "Invalid barcode";
                return;
            }

            var barcodeScanned = scannedBarcode.Barcode;
            var symbology = scannedBarcode.Symbology;

            ScannedBarcodeText = barcodeScanned;
        }
    }
}
