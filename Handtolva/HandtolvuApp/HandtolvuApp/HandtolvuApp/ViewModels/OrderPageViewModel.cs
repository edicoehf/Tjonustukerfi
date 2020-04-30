using HandtolvuApp.Controls;
using HandtolvuApp.Models;
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
                    //SelectedItem.OrderId = Order.Id;
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
                await App.OrderManager.CheckoutOrder(Order.Id);
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
