using HandtolvuApp.Controls;
using HandtolvuApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class OrderPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public OrderPageViewModel(Order o)
        {
            Order = o;

            SelectedItemChangedCommand = new Command(async () =>
            {
                SelectedItem.OrderId = Order.Id;
                var itemVM = new ItemViewModel(SelectedItem);
                var itemPage = new ItemPage();
                itemPage.BindingContext = itemVM;
                await App.Current.MainPage.Navigation.PushAsync(itemPage);
            });

            CheckoutCommand = new Command(async () =>
            {
                await App.ItemManager.CheckoutOrder(Order.Id);
            });
        }

        Item selectedItem;

        public Item SelectedItem
        {
            get => selectedItem;

            set
            {
                selectedItem = value;

                var args = new PropertyChangedEventArgs(nameof(SelectedItem));

                PropertyChanged?.Invoke(this, args);
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

                var args = new PropertyChangedEventArgs(nameof(Order));

                PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
