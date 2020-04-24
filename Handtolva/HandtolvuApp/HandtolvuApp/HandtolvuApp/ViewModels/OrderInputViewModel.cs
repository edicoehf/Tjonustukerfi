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
        public OrderInputViewModel(IScannerService scannerService) : base(scannerService)
        {
            Placeholder = "Sláðu inn pöntunarnúmer";

            ScannedBarcodeText = "";

            ClickCommand = new Command(async () =>
            {
                if(ScannedBarcodeText != null)
                {
                    Order order = await App.OrderManager.GetOrderAsync(ScannedBarcodeText);
                    if(order == null)
                    {
                        MessagingCenter.Send<OrderInputViewModel>(this, "NoOrder");
                        Placeholder = "Pöntunarnúmer er ekki til";
                        ScannedBarcodeText = "";
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

        public Command ClickCommand { get; }

    }
}
