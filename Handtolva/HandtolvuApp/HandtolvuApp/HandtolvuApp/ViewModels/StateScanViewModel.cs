using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using HandtolvuApp.Models.Json;
using Newtonsoft.Json;
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
                List<string> locations = await App.ItemManager.GetAllLoctions();
                List<State> states = await App.ItemManager.GetAllStates();
                
                if(locations.Count != 0 && states.Count != 0)
                {
                    string[] locationCheck = ScannedBarcodeText.Split('-');
                    if(locationCheck.Length == 2)
                    {
                        if (locations.Contains(locationCheck[1]) && states.Where(i => i.Name == locationCheck[0]).Count() > 0)
                        {
                            List<LocationStateChange> ret = await App.ItemManager.StateChangeByLocation(new List<LocationStateChange>() { new LocationStateChange { ItemBarcode = Item.Barcode, StateChangeBarcode = ScannedBarcodeText } });
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
                            MessagingCenter.Send<StateScanViewModel, string>(this, "Fail", $"Staðsetningar barkóðinn {ScannedBarcodeText} er ekki til");
                        }

                    }
                    else
                    {
                        MessagingCenter.Send<StateScanViewModel, string>(this, "Fail", $"Staðsetningar barkóðinn {ScannedBarcodeText} er ekki til");
                    }
                }
                else
                {
                    MessagingCenter.Send<StateScanViewModel, string>(this, "Fail", $"Það eru ekki til neinar staðsetningar til");
                }
            }
            else
            {
                Placeholder = "Staðsetning verður að vera gefin";
            }
        }
    }
}
