using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.FailRequestHandler;
using HandtolvuApp.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                AddToList();
            });

            RemoveCommand = new Command<string>((item) =>
            {
                AllItems.Remove(item);
            });

            SendCommand = new Command( async () =>
            {
                if(AllItems.Count > 0)
                {
                    var items = new List<LocationStateChange>();
                    List<LocationStateChange> invalidInput;

                    foreach(var i in AllItems)
                    {
                        items.Add(new LocationStateChange { ItemBarcode = i, StateChangeBarcode = Barcode });
                    }

                    if(CrossConnectivity.Current.IsConnected)
                    {
                        invalidInput = await App.ItemManager.StateChangeByLocation(items);
                        if(invalidInput.Count == 0)
                        {
                            // success
                            MessagingCenter.Send<LocationItemScanViewModel, string>(this, "Success", $"Allar vörur eru skannaðar í hólf {Barcode}");
                            AllItems.Clear();
                        }
                        else
                        {
                            // display alert that something failed
                            MessagingCenter.Send<LocationItemScanViewModel, string>(this, "Fail", $"Ekki var hægt að setja allar vörur í hólf {Barcode}.\n\nEftir er listi af vörum sem ekki var hægt að setja í hólfið");
                            AllItems.Clear();

                            foreach (LocationStateChange i in invalidInput)
                            {
                                AllItems.Add(i.ItemBarcode);
                            }
                        }
                    }
                    else
                    {
                        FailedRequstCollection.ItemFailedRequests.AddRange(items);
                        MessagingCenter.Send<LocationItemScanViewModel, string>(this, "Fail", "Handskanni ótengdur\n\nHægt er að fara á forsíðu og senda aftur þegar handskanni er tengdur");
                    }
                }
                else
                {
                    // must be some items on the list
                    MessagingCenter.Send<LocationItemScanViewModel, string>(this, "Fail", "Það verður að vera einhverjar vörur til að skanna í hólf");

                }
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

        public void AddToList()
        {
            if(!AllItems.Contains(ScannedBarcodeText))
            {
                AllItems.Insert(0, ScannedBarcodeText);
            }

            ScannedBarcodeText = "";
        }
    }
}
