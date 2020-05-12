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

            MessagingCenter.Subscribe<FailedRequestViewModel, string>(this, "Success", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Klárað!", message, "Ok");
                LoadingLayout.IsVisible = false;
                NavigationPage.SetHasNavigationBar(this, true);
            });

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
            MessagingCenter.Unsubscribe<FailedRequestViewModel, string>(this, "Success");
            MessagingCenter.Unsubscribe<FailedRequestViewModel, string>(this, "Fail");
            base.OnDisappearing();
        }

        public void ActivityShow(object sender, EventArgs e)
        {
            LoadingLayout.IsVisible = true;
            NavigationPage.SetHasNavigationBar(this, false);
            var vm = BindingContext as FailedRequestViewModel;
            vm.SendCommand.Execute(null);
        }

        public void RemoveClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selected = button.BindingContext as LocationStateChange ;
            var vm = BindingContext as FailedRequestViewModel;
            vm.RemoveCommand.Execute(selected);
        }

        public async void RemoveAllCall(object sender, EventArgs e)
        {
            var res = await App.Current.MainPage.DisplayAlert("Hætta við!", "Ertu viss að þú viljir hætta við allar færslur?\n\nEkki verður hægt að sækja listann aftur ef það er staðfest", "Staðfesta", "Hætta við");
            if(res)
            {
                var vm = BindingContext as FailedRequestViewModel;
                vm.RemoveAllCommand.Execute(null);
            }
        }
    }
}