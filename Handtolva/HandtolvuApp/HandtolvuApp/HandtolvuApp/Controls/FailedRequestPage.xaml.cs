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
    }
}