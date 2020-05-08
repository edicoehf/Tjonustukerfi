using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HandtolvuApp.Models.Json
{
    public class ItemJson : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string location;
        public string Location
        {
            get => location;

            set
            {
                location = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Location));
            }
        }

    }
}
