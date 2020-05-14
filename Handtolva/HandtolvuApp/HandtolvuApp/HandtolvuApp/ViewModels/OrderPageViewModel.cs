using HandtolvuApp.Controls;
using HandtolvuApp.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class OrderPageViewModel : BaseViewModel
    {
        public OrderPageViewModel(Order o)
        {
            Order = o;

            SelectedItemChangedCommand = new Command(async () =>
            {
                if(SelectedItem != null)
                {
                    // Navigate to the item page for selected item
                    var itemVM = new ItemViewModel(SelectedItem);
                    var itemPage = new ItemPage
                    {
                        BindingContext = itemVM
                    };
                    await App.Current.MainPage.Navigation.PushAsync(itemPage);
                }
            });

            CheckoutCommand = new Command(async () =>
            {
                // Check if device has connection
                if(CrossConnectivity.Current.IsConnected)
                {
                    // Check if order can be checkout out
                    if(await App.OrderManager.CheckoutOrder(Order.Id))
                    {
                        MessagingCenter.Send<OrderPageViewModel, string>(this, "Success", $"Allar vörur í pöntun {Order.Barcode} hafar verið skráðar sóttar");
                    }
                    else
                    {
                        MessagingCenter.Send<OrderPageViewModel, string>(this, "Fail", $"Ekki var hægt að skrá {Order.Barcode} sem sótta");
                    }
                }
                else
                {
                    MessagingCenter.Send<OrderPageViewModel, string>(this, "Fail", $"Handskanni er ekki tengdur");
                }
            });
        }

        Item selectedItem;

        public Item SelectedItem
        {
            get => selectedItem;

            set
            {
                selectedItem = value;

                NotifyPropertyChanged(nameof(SelectedItem));
            }
        }

        public Command SelectedItemChangedCommand { get; }

        public Command CheckoutCommand { get; }

        Order order;

        public Order Order
        {
            get => order;

            set
            {
                order = value;

                NotifyPropertyChanged(nameof(Order));
            }
        }
    }
}
