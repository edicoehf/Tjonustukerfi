using HandtolvuApp.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HandtolvuApp.ViewModels
{
    class MainPageViewModel
    {
        public MainPageViewModel()
        {
            OrderCommand = new Command(async () =>
            {
                // navigate to order scan page
                var orderInputVM = new OrderInputViewModel();
                var orderInputPage = new OrderInputPage();
                orderInputPage.BindingContext = orderInputVM;
                await Application.Current.MainPage.Navigation.PushAsync(orderInputPage);
            });

            ItemCommand = new Command(async () =>
            {
                // navigate to item scan page
                var itemInputVM = new ItemInputViewModel();
                var itemInputPage = new ItemInputPage();
                itemInputPage.BindingContext = itemInputVM;
                await App.Current.MainPage.Navigation.PushAsync(itemInputPage);
            });

            LocationCommand = new Command(async () =>
            {
                // navigate to location scan page
                var locationScanVM = new LocationScanViewModel();
                var locationScanPage = new LocationScanPage();
                locationScanPage.BindingContext = locationScanVM;
                await App.Current.MainPage.Navigation.PushAsync(locationScanPage);
            });

            FailedRequestCommand = new Command(async () =>
            {
                // navigate to failed requests page
                var failedRequestVM = new FailedRequestViewModel();
                var failedRequestPage = new FailedRequestPage();
                failedRequestPage.BindingContext = failedRequestVM;
                await App.Current.MainPage.Navigation.PushAsync(failedRequestPage);
            });
        }

        public Command OrderCommand { get; }
        public Command ItemCommand { get; }
        public Command FailedRequestCommand { get; }
        public Command LocationCommand { get; }
    }
}
