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

            MessagingCenter.Subscribe<LocationItemScanViewModel, string>(this, "Success", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Klárað!", message, "Ok");
                LoadingLayout.IsVisible = false;
                NavigationPage.SetHasNavigationBar(this, true);
            });

            MessagingCenter.Subscribe<LocationItemScanViewModel, string>(this, "Fail", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa!", message, "Ok");
                LoadingLayout.IsVisible = false;
                NavigationPage.SetHasNavigationBar(this, true);
            });

            MessagingCenter.Subscribe<ScannerViewModel>(this, "ScannedBarcode", (sender) =>
            { 
                vm.AddCommand.Execute(null);
                MyEditor.Focus();
            });

            vm.Init();

            MyEditor.Focus();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<LocationItemScanViewModel, string>(this, "Success");
            MessagingCenter.Unsubscribe<LocationItemScanViewModel, string>(this, "Fail");
            MessagingCenter.Unsubscribe<ScannerViewModel>(this, "ScannedBarcode");
            var vm = BindingContext as LocationItemScanViewModel;
            vm.DeInit();
            base.OnDisappearing();
        }

        public void RemoveClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selected = button.BindingContext as string;
            var vm = BindingContext as LocationItemScanViewModel;
            vm.RemoveCommand.Execute(selected);
        }

        public void ActivityShow(object sender, EventArgs e)
        {
            LoadingLayout.IsVisible = true;
            NavigationPage.SetHasNavigationBar(this, false);
            var vm = BindingContext as LocationItemScanViewModel;
            vm.SendCommand.Execute(null);
        }

        protected override void OnBindingContextChanged()
        {
            MyEditor.Focus();
            base.OnBindingContextChanged();
        }
    }
}