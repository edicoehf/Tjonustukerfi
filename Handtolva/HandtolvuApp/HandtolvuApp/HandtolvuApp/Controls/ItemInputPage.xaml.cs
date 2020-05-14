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
    public partial class ItemInputPage : ContentPage
    {
        public ItemInputPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MyEditor.Focus();
            var vm = BindingContext as ItemInputViewModel;

            // Need to subscribe every time the page appears
            MessagingCenter.Subscribe<ItemInputViewModel, string>(this, "Villa", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa", message, "Ok");
                MyEditor.Focus();
            });

            // Handles scanning from device and sends API request
            MessagingCenter.Subscribe<ScannerViewModel>(this, "ScannedBarcode", (sender) =>
            {
                vm.ClickCommand.Execute(null);
            });

            vm.Init();

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            // Need to unsubscribe everytime the page disappears
            MessagingCenter.Unsubscribe<ItemInputViewModel, string>(this, "Villa");
            MessagingCenter.Unsubscribe<ScannerViewModel>(this, "ScannedBarcode");
            var vm = BindingContext as ItemInputViewModel;
            vm.DeInit();
            base.OnDisappearing();
        }
    }
}