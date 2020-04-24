using HandtolvuApp.Controls;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ItemViewModel(Item i, NextStates n)
        {
            Item = i;

            NextStates = n;

            nextStates.NextAvailableStates.Reverse();

            ScanStateCommand = new Command(async () =>
            {
                // send user to scan site
                var scanPageVM = new StateScanViewModel(App.Scanner, item);
                var scanPage = new StateScanPage();
                scanPage.BindingContext = scanPageVM;
                await App.Current.MainPage.Navigation.PushAsync(scanPage);
            });
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

        NextStates nextStates;

        public NextStates NextStates
        {
            get => nextStates;

            set
            {
                nextStates = value;

                var args = new PropertyChangedEventArgs(nameof(NextStates));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command ScanStateCommand { get;  }

    }
}