using HandtolvuApp.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class LandingPageViewModel
    {
        public LandingPageViewModel()
        {
            OrderCommand = new Command(async () =>
            {
                var orderInputVM = new OrderInputViewModel();
                var orderInputPage = new OrderInputPage();
                orderInputPage.BindingContext = orderInputVM;
                await Application.Current.MainPage.Navigation.PushAsync(orderInputPage);
            });

            ItemCommand = new Command(async () =>
            {
                var itemInputVM = new ItemInputViewModel();
                var itemInputPage = new ItemInputPage();
                itemInputPage.BindingContext = itemInputVM;
                await App.Current.MainPage.Navigation.PushAsync(itemInputPage);
            });
        }

        public Command OrderCommand { get; }
        public Command ItemCommand { get; }
    }
}
