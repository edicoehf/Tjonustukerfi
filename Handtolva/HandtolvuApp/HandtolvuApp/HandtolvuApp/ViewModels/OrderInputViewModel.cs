using HandtolvuApp.Controls;
using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class OrderInputViewModel : ScannerViewModel
    {
        public OrderInputViewModel() : base()
        {
            Placeholder = "Sláðu inn pöntunarnúmer";

            ScannedBarcodeText = "";

            FindOrderCommand = new Command(async () =>
            {
                if(ScannedBarcodeText != "")
                {
                    Order order = await App.OrderManager.GetOrderAsync(ScannedBarcodeText);
                    if(order == null)
                    {
                        MessagingCenter.Send<OrderInputViewModel, string>(this, "NoOrder", ScannedBarcodeText);
                        Placeholder = "Pöntunarnúmer er ekki til";
                        ScannedBarcodeText = "";
                    }
                    else
                    {
                        var orderVM = new OrderPageViewModel(order);
                        var orderPage = new OrderPage
                        {
                            BindingContext = orderVM
                        };
                        await App.Current.MainPage.Navigation.PushAsync(orderPage);
                    }
                }
                else
                {
                    // athuga lengd a message, er of langt fyrir nuverandi staerd
                    Placeholder = "Pöntunarnúmer verður að vera gefið";
                }
            });
        }

        string input;

        public string Input
        {
            get => input;
            set
            {
                input = value;
                NotifyPropertyChanged(nameof(Input));
            }
        }

        public Command FindOrderCommand { get; }

    }
}
