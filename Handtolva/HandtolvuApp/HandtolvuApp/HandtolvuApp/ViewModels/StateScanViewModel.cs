using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using HandtolvuApp.Models.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                if(ScannedBarcodeText != "")
                {
                    StateChange();
                }
                else
                {
                    Placeholder = "Staðsetning verður að vera gefin";
                }
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
            if (await App.ItemManager.StateChangeWithId(Item.Id, ScannedBarcodeText))
            {
                MessagingCenter.Send<StateScanViewModel, string>(this, "Success", $"{Item.Barcode} hefur verið skannað í hólf {ScannedBarcodeText}");
            }
            else
            {
                MessagingCenter.Send<StateScanViewModel, string>(this, "Fail", $"Ekki var hægt að skanna {Item.Barcode} í hólf {ScannedBarcodeText}");
            }
        }
    }
}
