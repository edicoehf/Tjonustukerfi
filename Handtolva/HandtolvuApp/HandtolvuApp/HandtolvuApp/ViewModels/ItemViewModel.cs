using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ItemViewModel(Item i)
        {
            Item = i;
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

        Command DismissCommand { get;  }

    }
}