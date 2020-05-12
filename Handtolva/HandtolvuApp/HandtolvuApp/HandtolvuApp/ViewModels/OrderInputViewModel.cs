using HandtolvuApp.Controls;
using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Models;
using Plugin.Connectivity;
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
                // Check if there is any input
                if(ScannedBarcodeText != "")
                {
                    // Check if device is connected to internet
                    if(CrossConnectivity.Current.IsConnected)
                    {
                        // Get order from server - null if order was not found
                        Order order = await App.OrderManager.GetOrderAsync(ScannedBarcodeText);
                        if(order == null)
                        {
                            // Message fail message
                            MessagingCenter.Send<OrderInputViewModel, string>(this, "Fail", $"Pöntunarnúmer {ScannedBarcodeText} er ekki til");
                            Placeholder = "Pöntunarnúmer er ekki til";
                            ScannedBarcodeText = "";
                        }
                        else
                        {
                            // navigate to order page
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
                        MessagingCenter.Send<OrderInputViewModel, string>(this, "Fail", "Handskanni er ekki tengdur");
                    }
                }
                else
                {
                    // athuga lengd a message, er of langt fyrir nuverandi staerd
                    Placeholder = "Pöntunarnúmer vantar";
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
