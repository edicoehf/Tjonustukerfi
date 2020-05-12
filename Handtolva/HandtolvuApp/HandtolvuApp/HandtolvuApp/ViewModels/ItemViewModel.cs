using HandtolvuApp.Controls;
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

                ScanStateCommand = new Command(async () =>
                {
                    // send user to scan state site
                    var scanPageVM = new StateScanViewModel(item);
                    var scanPage = new StateScanPage
                    {
                        BindingContext = scanPageVM
                    };
                    await App.Current.MainPage.Navigation.PushAsync(scanPage);
                });
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

        /// <summary>
        ///     Update value for item and next states
        /// </summary>
        public async void UpdateViewModel()
        {
            if(Item != null)
            {
                Item = await App.ItemManager.GetItemAsync(Item.Barcode);
                NextStates = await App.ItemManager.GetNextStatesAsync(Item.Barcode);
            }
        }

    }
}