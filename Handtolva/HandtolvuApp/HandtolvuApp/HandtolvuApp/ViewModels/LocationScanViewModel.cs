using HandtolvuApp.Controls;
using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
                if(ScannedBarcodeText != "")
                {

                    List<string> locations = await App.ItemManager.GetAllLoctions();
                    List<State> states = await App.ItemManager.GetAllStates();

                    if (locations.Count != 0 && states.Count != 0)
                    {
                        string[] locationCheck = ScannedBarcodeText.Split('-');

                        if(locationCheck.Length == 2)
                        {
                            if(locations.Contains(locationCheck[1]) && states.Where(i => i.Name == locationCheck[0]).Count() > 0)
                            {
                                var locationItemScanVM = new LocationItemScanViewModel(ScannedBarcodeText);
                                var locationItemScanPage = new LocationItemScanPage();
                                locationItemScanPage.BindingContext = locationItemScanVM;
                                await App.Current.MainPage.Navigation.PushAsync(locationItemScanPage);

                            }
                            else
                            {
                                MessagingCenter.Send<LocationScanViewModel, string>(this, "Fail", $"Staðsetningar barkóðinn {ScannedBarcodeText} er ekki til");
                                // not a valid location
                            }
                        }
                        else
                        {
                            MessagingCenter.Send<LocationScanViewModel, string>(this, "Fail", $"Staðsetningar barkóðinn {ScannedBarcodeText} er ekki til");
                            // barcode not in correct format
                        }

                    }
                    else
                    {
                        MessagingCenter.Send<LocationScanViewModel, string>(this, "Fail", $"Það eru ekki til neinar staðsetningar til");
                        // can not find state or location
                    }

                }
                else
                {
                    Placeholder = "Staðsetning verður að vera gefin";
                }
            });
        }

        public Command ClickCommand { get; }
    }
}
