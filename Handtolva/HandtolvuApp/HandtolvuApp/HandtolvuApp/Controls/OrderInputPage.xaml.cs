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
    public partial class OrderInputPage : ContentPage
    {
        public OrderInputPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MyEditor.Focus();
            var vm = BindingContext as OrderInputViewModel;
            MessagingCenter.Subscribe<OrderInputViewModel, string>(this, "Fail", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa", message, "Ok");
            });

            MessagingCenter.Subscribe<ScannerViewModel>(this, "ScannedBarcode", (sender) =>
            {
                vm.FindOrderCommand.Execute(null);
            });

            vm.Init();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<OrderInputViewModel, string>(this, "Fail");
            MessagingCenter.Unsubscribe<ScannerViewModel>(this, "ScannedBarcode");
            var vm = BindingContext as OrderInputViewModel;
            vm.DeInit();
            MyEditor.Text = "";
            base.OnDisappearing();
        }
    }
}