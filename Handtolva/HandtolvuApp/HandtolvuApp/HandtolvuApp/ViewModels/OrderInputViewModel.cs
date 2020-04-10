using HandtolvuApp.Controls;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class OrderInputViewModel : INotifyPropertyChanged
    {
        public OrderInputViewModel()
        {
            Placeholder = "Sláðu inn pöntunarnúmer";

            ClickCommand = new Command(async () =>
            {
                if(InputVariable != null)
                {
                    Order order = await App.ItemManager.GetOrderAsync(inputVariable);
                    if(order == null)
                    {
                        MessagingCenter.Send<OrderInputViewModel>(this, "NoOrder");
                        Placeholder = "Pöntunarnúmer er ekki til";
                    }
                    else
                    {
                        var orderVM = new OrderPageViewModel(order);
                        var orderPage = new OrderPage();
                        orderPage.BindingContext = orderVM;
                        await App.Current.MainPage.Navigation.PushAsync(orderPage);
                    }
                }
                else
                {
                    Placeholder = "Pöntunarnúmer verður að vera gefið";
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
