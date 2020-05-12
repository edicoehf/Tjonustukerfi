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

        public async void StateChange()
        {
            if(ScannedBarcodeText != "")
            {
                if(await App.InfoManager.CheckLocationBarcode(ScannedBarcodeText))
                {
                    List<LocationStateChange> sendChange = new List<LocationStateChange>() { new LocationStateChange { ItemBarcode = Item.Barcode, StateChangeBarcode = ScannedBarcodeText } };
                    if (CrossConnectivity.Current.IsConnected)
                    {
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
                        // handle no connection
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
