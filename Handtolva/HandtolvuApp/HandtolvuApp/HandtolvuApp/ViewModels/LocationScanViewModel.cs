using HandtolvuApp.Controls;
using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if(ScannedBarcodeText != "")
                {
                    // Check if it is a valid state/location
                    if(await App.InfoManager.CheckLocationBarcode(ScannedBarcodeText))
                    {
                        // navigate user to scan item page
                        var locationItemScanVM = new LocationItemScanViewModel(ScannedBarcodeText);
                        var locationItemScanPage = new LocationItemScanPage();
                        locationItemScanPage.BindingContext = locationItemScanVM;
                        await App.Current.MainPage.Navigation.PushAsync(locationItemScanPage);
                    }
                    else
                    {
                        // Check if it failed because of lack of internet connection
                        if(CrossConnectivity.Current.IsConnected)
                        {
                            MessagingCenter.Send<LocationScanViewModel, string>(this, "Fail", $"Staðsetningar barkóðinn {ScannedBarcodeText} er ekki til");
                        }
                        else
                        {
                            MessagingCenter.Send<LocationScanViewModel, string>(this, "Fail", $"Handskanni er ekki tengdur");
                        }
                    }

                    ScannedBarcodeText = "";
                }
                else
                {

                    Placeholder = "Staðsetningu vantar";
                }
            });
        }

        public Command ClickCommand { get; }

    }
}
