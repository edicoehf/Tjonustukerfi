using HandtolvuApp.Controls;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class ItemInputViewModel : INotifyPropertyChanged
    {
        public ItemInputViewModel()
        {

            Placeholder = "Sláðu inn vörunúmer";

            ClickCommand = new Command(async () =>
            {
                if(InputVariable != null)
                {
                    Item item = await App.ItemManager.GetItemAsync(inputVariable);
                    if(item == null)
                    {
                        // handle that there is no item with this barcode
                        MessagingCenter.Send<ItemInputViewModel>(this, "Villa");
                        Placeholder = "Vörunúmer er ekki til";
                    }
                    else
                    {
                        NextStates n = await App.ItemManager.GetNextStatesAsync(inputVariable);
                        item.Barcode = inputVariable;
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
