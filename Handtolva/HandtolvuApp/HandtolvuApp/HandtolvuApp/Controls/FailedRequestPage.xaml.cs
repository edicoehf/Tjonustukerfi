using HandtolvuApp.Models;
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
    public partial class FailedRequestPage : ContentPage
    {
        public FailedRequestPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var vm = BindingContext as FailedRequestViewModel;

            // Handle successful update sent to API
            MessagingCenter.Subscribe<FailedRequestViewModel, string>(this, "Success", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Klárað!", message, "Ok");
                LoadingLayout.IsVisible = false;
                NavigationPage.SetHasNavigationBar(this, true);
            });

            // Handles fail message when sending update to API
            MessagingCenter.Subscribe<FailedRequestViewModel, string>(this, "Fail", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa!", message, "Ok");
                LoadingLayout.IsVisible = false;
                NavigationPage.SetHasNavigationBar(this, true);
            });

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            // Unsubscribe every time user navigates from the page
            MessagingCenter.Unsubscribe<FailedRequestViewModel, string>(this, "Success");
            MessagingCenter.Unsubscribe<FailedRequestViewModel, string>(this, "Fail");
            base.OnDisappearing();
        }

        // Handles Button press on page for "Klara" button
        public void ActivityShow(object sender, EventArgs e)
        {
            LoadingLayout.IsVisible = true;
            NavigationPage.SetHasNavigationBar(this, false);
            var vm = BindingContext as FailedRequestViewModel;
            vm.SendCommand.Execute(null);
        }

        // Handles Button press on page for "x" button for each item
        public void RemoveClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selected = button.BindingContext as LocationStateChange;  // Gets the context of the individual LocationStateChange of the collection view
            var vm = BindingContext as FailedRequestViewModel; // Gets the whole ViewModelContext and sets it as BindingContext
            vm.RemoveCommand.Execute(selected);
        }

        // Handles "Haetta vid" button press
        public async void RemoveAllCall(object sender, EventArgs e)
        {
            // Sends alert to user asking for confirmation res = true if he presses accept
            var res = await App.Current.MainPage.DisplayAlert("Hætta við!", "Ertu viss að þú viljir hætta við allar færslur?\n\nEkki verður hægt að sækja listann aftur ef það er staðfest", "Staðfesta", "Hætta við");
            if(res)
            {
                var vm = BindingContext as FailedRequestViewModel;
                vm.RemoveAllCommand.Execute(null);
            }
        }
    }
}