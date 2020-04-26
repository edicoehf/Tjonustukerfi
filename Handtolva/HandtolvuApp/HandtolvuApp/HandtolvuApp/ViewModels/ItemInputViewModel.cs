using HandtolvuApp.Controls;
using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
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

            ClickCommand = new Command(async () =>
            {
                if(ScannedBarcodeText != null)
                {
                    Item item = await App.ItemManager.GetItemAsync(ScannedBarcodeText);
                    if(item == null)
                    {
                        // handle that there is no item with this barcode
                        MessagingCenter.Send<ItemInputViewModel>(this, "Villa");
                        Placeholder = "Vörunúmer er ekki til";
                        ScannedBarcodeText = "";
                    }
                    else
                    {
                        NextStates n = await App.ItemManager.GetNextStatesAsync(ScannedBarcodeText);
                        item.Barcode = ScannedBarcodeText;
                        var itemVM = new ItemViewModel(item, n);
                        var itemPage = new ItemPage();
                        itemPage.BindingContext = itemVM;
                        await Application.Current.MainPage.Navigation.PushAsync(itemPage);
                    }
                }
                else
                {
                    Placeholder = "Vörunúmer verður að vera gefið";
                }
            });
        }

        public Command ClickCommand { get; }

    }
}
