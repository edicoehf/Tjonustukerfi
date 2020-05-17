using HandtolvuApp.Controls;
using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class ItemInputViewModel : ScannerViewModel
    {
        public ItemInputViewModel() : base()
        {
            ScannedBarcodeText = "";

            Placeholder = "Sláðu inn vörunúmer";

            ClickCommand = new Command(() =>
            {
                ScannedItem();
            });
        }

        /// <summary>
        ///     Command to send a request to server
        /// </summary>
        public Command ClickCommand { get; }

        /// <summary>
        ///     Handle the API request to server
        /// </summary>
        public async void ScannedItem()
        {
            // Check if there is some input
            if (ScannedBarcodeText != "")
            {
                // Check for internet connection
                if(CrossConnectivity.Current.IsConnected)
                {
                    // Send request to server
                    Item item = await App.ItemManager.GetItemAsync(ScannedBarcodeText);
                    if (item == null)
                    {
                        // handle that there is no item with this barcode
                        MessagingCenter.Send<ItemInputViewModel, string>(this, "Villa", $"Vörunúmer {ScannedBarcodeText} er ekki til");
                        Placeholder = "Vörunúmer er ekki til";
                        ScannedBarcodeText = "";
                    }
                    else
                    {
                        // Navigate to new page for given item
                        item.Barcode = ScannedBarcodeText;
                        var itemVM = new ItemViewModel(item);
                        var itemPage = new ItemPage
                        {
                            BindingContext = itemVM
                        };
                        await Application.Current.MainPage.Navigation.PushAsync(itemPage);
                        ScannedBarcodeText = "";
                    }
                }
                else
                {
                    // Message that device is not connected to internet
                    MessagingCenter.Send<ItemInputViewModel, string>(this, "Villa", $"Handskanni er ekki tengdur");
                }
            }
            else
            {
                Placeholder = "Vörunúmer vantar";
            }
        }

    }
}
