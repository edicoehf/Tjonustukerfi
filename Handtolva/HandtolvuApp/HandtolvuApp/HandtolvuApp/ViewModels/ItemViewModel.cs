﻿using HandtolvuApp.Controls;
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
    public class ItemViewModel : BaseViewModel
    {
        public ItemViewModel(Item i)
        {
            if(i != null)
            {
                Item = i;

                ItemJson = JsonConvert.DeserializeObject<ItemJson>(i.Json);

                ScanStateCommand = new Command(async () =>
                {
                    // send user to scan site
                    var scanPageVM = new StateScanViewModel(item);
                    var scanPage = new StateScanPage
                    {
                        BindingContext = scanPageVM
                    };
                    await App.Current.MainPage.Navigation.PushAsync(scanPage);
                });
            }

        }

        ItemJson itemJson;

        public ItemJson ItemJson
        {
            get => itemJson;

            set
            {
                itemJson = value;

                NotifyPropertyChanged(nameof(ItemJson));
            }
        }

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

        NextStates nextStates;

        public NextStates NextStates
        {
            get => nextStates;

            set
            {
                nextStates = value;

                NotifyPropertyChanged(nameof(NextStates));
            }
        }

        public Command ScanStateCommand { get;  }

        public async void UpdateViewModel()
        {
            if(Item != null)
            {
                Item = await App.ItemManager.GetItemAsync(Item.Barcode);
                ItemJson = JsonConvert.DeserializeObject<ItemJson>(Item.Json);
                NextStates = await App.ItemManager.GetNextStatesAsync(Item.Barcode);
            }
        }

    }
}