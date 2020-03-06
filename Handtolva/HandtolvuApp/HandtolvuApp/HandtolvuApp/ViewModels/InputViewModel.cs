using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HandtolvuApp.ViewModels
{
    class InputViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string inputVariable;

        public string InputVariable
        {
            get => inputVariable;

            set
            {
                inputVariable = value;

                var args = new PropertyChangedEventArgs(nameof(InputVariable));

                PropertyChanged?.Invoke(this, args);

                System.Diagnostics.Debug.WriteLine("Input: " + inputVariable);
            }
        }
    }
}
