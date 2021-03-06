﻿using HandtolvuApp.Data.Implementations;
using HandtolvuApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandtolvuApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            // Message that handles success requests
            MessagingCenter.Subscribe<OrderPageViewModel, string>(this, "Success", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Klárað", message, "Ok");
            });

            // Message that handles failed requests
            MessagingCenter.Subscribe<OrderPageViewModel, string>(this, "Fail", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa", message, "Ok");
            });

            MyCollectionView.SelectedItem = null;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            // Unsubscribe from all messages
            MessagingCenter.Unsubscribe<OrderPageViewModel, string>(this, "Success");
            MessagingCenter.Unsubscribe<OrderPageViewModel, string>(this, "Fail");
            base.OnDisappearing();
        }
    }
}