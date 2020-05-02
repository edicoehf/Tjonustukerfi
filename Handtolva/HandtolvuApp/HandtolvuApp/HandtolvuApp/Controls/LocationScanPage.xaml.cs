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
    public partial class LocationScanPage : ContentPage
    {
        public LocationScanPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<LocationScanViewModel, string>(this, "Fail", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa!", message, "Ok");
                MyEditor.Focus();
            });

            MyEditor.Focus();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<LocationScanViewModel, string>(this, "Fail");
            base.OnDisappearing();
        }
    }
}