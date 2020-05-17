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
    public partial class LocationItemScanPage : ContentPage
    {
        public LocationItemScanPage()
        {
            InitializeComponent();

            this.MyEditor.ReturnCommand = new Command(() =>
            {
                var vm = BindingContext as LocationItemScanViewModel;
                vm.AddCommand.Execute(null);
                MyEditor.Focus();
            });
        }

        protected override void OnAppearing()
        { 
            var vm = BindingContext as LocationItemScanViewModel;

            // Message that handles success
            MessagingCenter.Subscribe<LocationItemScanViewModel, string>(this, "Success", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Klárað!", message, "Ok");
                LoadingLayout.IsVisible = false;
                NavigationPage.SetHasNavigationBar(this, true);
            });

            // Message that handles failure
            MessagingCenter.Subscribe<LocationItemScanViewModel, string>(this, "Fail", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa!", message, "Ok");
                LoadingLayout.IsVisible = false;
                NavigationPage.SetHasNavigationBar(this, true);
            });

            // Message that handles barcode scanned from device
            MessagingCenter.Subscribe<ScannerViewModel>(this, "ScannedBarcode", (sender) =>
            { 
                vm.AddCommand.Execute(null);
                MyEditor.Focus();
            });

            vm.Init(); // Initialize scanner from device

            MyEditor.Focus();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            // Unsubscribe from all messages
            MessagingCenter.Unsubscribe<LocationItemScanViewModel, string>(this, "Success");
            MessagingCenter.Unsubscribe<LocationItemScanViewModel, string>(this, "Fail");
            MessagingCenter.Unsubscribe<ScannerViewModel>(this, "ScannedBarcode");
            var vm = BindingContext as LocationItemScanViewModel;
            vm.DeInit(); // DeInitialize scanner from device
            base.OnDisappearing();
        }

        // Handles remove item from list asociated with each Item
        public void RemoveClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selected = button.BindingContext as string;
            var vm = BindingContext as LocationItemScanViewModel;
            vm.RemoveCommand.Execute(selected);
        }

        // Handles showing activity loading and sending request to server
        public void ActivityShow(object sender, EventArgs e)
        {
            LoadingLayout.IsVisible = true;
            NavigationPage.SetHasNavigationBar(this, false);
            var vm = BindingContext as LocationItemScanViewModel;
            vm.SendCommand.Execute(null);
        }

        // If there is a change in bound context refocus on editor
        protected override void OnBindingContextChanged()
        {
            MyEditor.Focus();
            base.OnBindingContextChanged();
        }
    }
}