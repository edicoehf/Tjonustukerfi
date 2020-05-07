using HandtolvuApp.Data.Interfaces;
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
    }
}
