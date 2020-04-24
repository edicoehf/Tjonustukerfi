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
    class LandingPageViewModel
    {
        public LandingPageViewModel()
        {
            OrderCommand = new Command(async () =>
            {
                var orderInputVM = new OrderInputViewModel(App.Scanner);
                var orderInputPage = new OrderInputPage();
                orderInputPage.BindingContext = orderInputVM;
                await Application.Current.MainPage.Navigation.PushAsync(orderInputPage);
            });

            ItemCommand = new Command(async () =>
            {
                var itemInputVM = new ItemInputViewModel(App.Scanner);
                var itemInputPage = new ItemInputPage();
                itemInputPage.BindingContext = itemInputVM;
                await App.Current.MainPage.Navigation.PushAsync(itemInputPage);
            });

            LocationCommand = new Command(async () =>
            {
                var locationScanVM = new LocationScanViewModel(App.Scanner);
                var locationScanPage = new LocationScanPage();
                locationScanPage.BindingContext = locationScanVM;
                await App.Current.MainPage.Navigation.PushAsync(locationScanPage);
            });

            TestCommand = new Command(async () =>
            {
                NextStates nextStates = await App.ItemManager.GetNextStatesAsync("50500004");

                if(nextStates == null)
                {
                    Debug.WriteLine("Did not work :/");
                }
                else
                {
                    Debug.WriteLine(nextStates.ToString());
                }
            });
        }

        public Command OrderCommand { get; }
        public Command ItemCommand { get; }
        public Command TestCommand { get; }

        public Command LocationCommand { get; }
    }
}
