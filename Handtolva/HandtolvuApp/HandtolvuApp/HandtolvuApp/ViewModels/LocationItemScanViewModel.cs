using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class LocationItemScanViewModel : ScannerViewModel
    {
        public LocationItemScanViewModel(string barcode) : base()
        {

            Barcode = barcode;

            Placeholder = "Sláðu inn vörunúmer";

            ScannedBarcodeText = "";

            AllItems = new ObservableCollection<string>();

            AddCommand = new Command( () =>
            {
                AllItems.Insert(0, ScannedBarcodeText);
                ScannedBarcodeText = "";
            });

            RemoveCommand = new Command<string>((item) =>
            {
                AllItems.Remove(item);
            });

            SendCommand = new Command( async () =>
            {
                await App.ItemManager.StateChangeByLocation(AllItems, Barcode);
            });
        }

        string barcode;

        public string Barcode
        {
            get => barcode;

            set
            {
                barcode = value;

                NotifyPropertyChanged(nameof(Barcode));

            }
        }

        public Command AddCommand { get; }

        public Command RemoveCommand { get; }

        public Command SendCommand { get; }

        public ObservableCollection<string> AllItems { get; set; }
    }
}
