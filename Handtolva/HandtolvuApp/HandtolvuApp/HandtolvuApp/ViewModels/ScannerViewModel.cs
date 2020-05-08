using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

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

        public ScannerViewModel()
        {
            
        }

        public void Init()
        {
            var scanner = App.Scanner;
            scanner.Enable();
            scanner.OnScanDataCollected += ScannedDataCollected;
            scanner.OnStatusChanged += ScannedStatusChanged;

            var config = new ZebraScannerConfig();
            scanner.SetConfig(config);
        }

        public void DeInit()
        {
            var scanner = App.Scanner;

            if(scanner != null)
            {
                scanner.Disable();
                scanner.OnScanDataCollected -= ScannedDataCollected;
                scanner.OnStatusChanged -= ScannedStatusChanged;
            }
        }

        private void ScannedDataCollected(Object sender, StatusEventArgs e_status)
        {
            ScannedBarcodeText = e_status.Data;
            // if we want to handle barcode and symbology
            Barcode Barcode = new Barcode
            {
                Data = e_status.Data,
                Symbol = e_status.BarcodeType
            };

            MessagingCenter.Send<ScannerViewModel>(this, "ScannedBarcode");
        }

        private void ScannedStatusChanged(Object sender, string message)
        {
            string status = message;
        }
    }
}
