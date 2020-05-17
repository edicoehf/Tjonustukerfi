using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Extensions;
using HandtolvuApp.FailRequestHandler;
using HandtolvuApp.Models;
using HandtolvuApp.Models.Json;
using Newtonsoft.Json;
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
    class StateScanViewModel : ScannerViewModel
    {

        public StateScanViewModel(Item i) : base()
        {

            Item = i;

            Placeholder = "Sláðu inn stöðu";

            ScannedBarcodeText = "";

            StateScan = new Command(() =>
            {
                StateChange();
            });
        }

        public Command StateScan { get; }

        Item item;

        public Item Item
        {
            get => item;

            set
            {
                item = value;

                NotifyPropertyChanged(nameof(Item));
            }
        }

        /// <summary>
        /// Used to handle api call to change states given the barcode scanned/typed
        /// </summary>
        public async void StateChange()
        {
            // check if there is any input
            if(ScannedBarcodeText != "")
            {
                // Check if location is valid
                if(await App.InfoManager.CheckLocationBarcode(ScannedBarcodeText))
                {
                    // set data to right format to be sent to API
                    List<LocationStateChange> sendChange = new List<LocationStateChange>() { new LocationStateChange { ItemBarcode = Item.Barcode, StateChangeBarcode = ScannedBarcodeText } };
                    
                    // Check if device is connected to internet
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        // Send data to server
                        List<LocationStateChange> ret = await App.ItemManager.StateChangeByLocation(sendChange);
                        if (ret.Count == 0)
                        {
                            MessagingCenter.Send<StateScanViewModel, string>(this, "Success", $"{Item.Barcode} hefur verið skannað í hólf {ScannedBarcodeText}");
                        }
                        else
                        {
                            MessagingCenter.Send<StateScanViewModel, string>(this, "Fail", $"Ekki var hægt að skanna {Item.Barcode} í hólf {ScannedBarcodeText}");
                        }
                    }
                    else
                    {
                        // handle no connection adding failed request to list
                        FailedRequstCollection.ItemFailedRequests.AddOrUpdate<LocationStateChange>(sendChange);
                        MessagingCenter.Send<StateScanViewModel, string>(this, "Fail", "Handskanni ótengdur\n\nHægt er að fara á forsíðu og senda aftur þegar handskanni er tengdur");
                    }
                }
                else
                {
                    MessagingCenter.Send<StateScanViewModel, string>(this, "Fail", $"Staðsetningar barkóðinn {ScannedBarcodeText} er ekki til");
                }
            }
            else
            {
                Placeholder = "Staðsetningu vantar";
            }
        }
    }
}
