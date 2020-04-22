using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class LocationScanViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LocationScanViewModel()
        {
            Placeholder = "Sláðu inn vörunúmer";

            ClickCommand = new Command(async () =>
            {
                // handle scan location
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

        public Command ClickCommand { get; }

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
    }
}
