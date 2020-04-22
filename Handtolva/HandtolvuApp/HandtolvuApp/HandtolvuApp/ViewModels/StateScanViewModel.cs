using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class StateScanViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public StateScanViewModel(Item i)
        {

            Item = i;

            Placeholder = "Sláðu inn stöðu";

            StateScan = new Command(async () =>
            {
                await App.ItemManager.StateChangeWithId(Item.Id, InputVariable);
            });
        }

        string inputVariable;

        public string InputVariable
        {
            get => inputVariable;

            set
            {
                inputVariable = value;

                var args = new PropertyChangedEventArgs(nameof(InputVariable));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command StateScan { get; }

        string placeholder;

        public string Placeholder
        {
            get => placeholder;

            set
            {
                placeholder = value;

                var args = new PropertyChangedEventArgs(nameof(Placeholder));

                PropertyChanged?.Invoke(this, args);
            }
        }

        Item item;

        public Item Item
        {
            get => item;

            set
            {
                item = value;

                var args = new PropertyChangedEventArgs(nameof(Item));

                PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
