using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
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

            StateScan = new Command(async () =>
            {
                await App.ItemManager.StateChangeWithId(Item.Id, ScannedBarcodeText);
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
    }
}
