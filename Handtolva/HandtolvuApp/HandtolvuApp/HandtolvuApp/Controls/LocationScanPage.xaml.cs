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
            MessagingCenter.Subscribe<LocationScanViewModel, string>(this, "Fail", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa!", message, "Ok");
                MyEditor.Focus();
            });

            MessagingCenter.Subscribe<ScannerViewModel>(this, "ScannedBarcode", (sender) =>
            {
                vm.ClickCommand.Execute(null);
            });

            vm.Init();

            MyEditor.Focus();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<LocationScanViewModel, string>(this, "Fail");
            MessagingCenter.Unsubscribe<ScannerViewModel>(this, "ScannedBarcode");
            var vm = BindingContext as LocationScanViewModel;
            vm.DeInit();
            base.OnDisappearing();
        }
    }
}