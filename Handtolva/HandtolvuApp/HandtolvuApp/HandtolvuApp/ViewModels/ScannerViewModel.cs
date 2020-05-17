using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    /// <summary>
    ///     Handles scanning from device
    ///     
    ///     All view models using scanner need to inherit this class
    /// </summary>
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

        /// <summary>
        ///     Initialize the scanner and set events
        /// </summary>
        public void Init()
        {
            var scanner = App.Scanner;
            scanner.Enable();
            scanner.OnScanDataCollected += ScannedDataCollected;
            scanner.OnStatusChanged += ScannedStatusChanged;

            var config = new ZebraScannerConfig();
            scanner.SetConfig(config);
        }

        /// <summary>
        ///     Deinitialize scanner and remove events
        /// </summary>
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

        /// <summary>
        ///             Handle scanned data from device
        /// </summary>
        /// <param name="sender">Device that sends the data</param>
        /// <param name="e_status">The arguments sent with event, contains the data for the barcode</param>
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

        /// <summary>
        ///     Used if status of the scanning device is required
        /// </summary>
        /// <param name="sender">device that sends the data</param>
        /// <param name="message">The status message</param>
        private void ScannedStatusChanged(Object sender, string message)
        {
            string status = message;
        }
    }
}
