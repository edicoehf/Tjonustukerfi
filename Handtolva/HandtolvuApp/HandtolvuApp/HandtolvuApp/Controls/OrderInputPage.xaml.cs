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

            MessagingCenter.Subscribe<OrderInputViewModel, string>(this, "NoOrder", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa", $"Pöntunarnúmer {message} er ekki til", "Ok");
            });

            MessagingCenter.Subscribe<ScannerViewModel>(this, "BarcodeScanned", async (sender) =>
            {
                await App.Current.MainPage.DisplayAlert("Scanned", "Successful scan", "Ok");
                var vm = BindingContext as OrderInputViewModel;
                vm.FindOrderCommand.Execute(null);
            });

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<OrderInputViewModel, string>(this, "NoOrder");
            MessagingCenter.Unsubscribe<ScannerViewModel>(this, "BarcodeScanned");
            base.OnDisappearing();
        }
    }
}