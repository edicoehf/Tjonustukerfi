using HandtolvuApp.Controls;
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

        public LocationScanViewModel() : base() 
        {
            Placeholder = "Sláðu inn vörunúmer";

            ScannedBarcodeText = "";

            ClickCommand = new Command(async () =>
            {
                // TODO Check if valid barcode needed

                var locationItemScanVM = new LocationItemScanViewModel(ScannedBarcodeText);
                var locationItemScanPage = new LocationItemScanPage();
                locationItemScanPage.BindingContext = locationItemScanVM;
                await App.Current.MainPage.Navigation.PushAsync(locationItemScanPage);
            });
        }

        public Command ClickCommand { get; }
    }
}
