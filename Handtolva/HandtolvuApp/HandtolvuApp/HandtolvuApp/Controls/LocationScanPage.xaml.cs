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
            var vm = BindingContext as LocationScanViewModel;
            // Message that handles failed requests
            MessagingCenter.Subscribe<LocationScanViewModel, string>(this, "Fail", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa!", message, "Ok");
                MyEditor.Focus();
            });

            // Handle barcode scanned from device
            MessagingCenter.Subscribe<ScannerViewModel>(this, "ScannedBarcode", (sender) =>
            {
                vm.ClickCommand.Execute(null);
            });

            // Initialize scanner from device
            vm.Init();

            MyEditor.Focus();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            // Unsubscribe from all messages and DeInitialize scanner
            MessagingCenter.Unsubscribe<LocationScanViewModel, string>(this, "Fail");
            MessagingCenter.Unsubscribe<ScannerViewModel>(this, "ScannedBarcode");
            var vm = BindingContext as LocationScanViewModel;
            vm.DeInit();
            base.OnDisappearing();
        }
    }
}