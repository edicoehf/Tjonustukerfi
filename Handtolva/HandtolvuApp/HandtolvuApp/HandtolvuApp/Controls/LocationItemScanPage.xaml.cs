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
            MessagingCenter.Subscribe<LocationItemScanViewModel, string>(this, "Success", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Klárað!", message, "Ok");
            });

            MessagingCenter.Subscribe<LocationItemScanViewModel, string>(this, "Fail", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa!", message, "Ok");
            });

            MyEditor.Focus();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<LocationItemScanViewModel, string>(this, "Success");
            MessagingCenter.Unsubscribe<LocationItemScanViewModel, string>(this, "Fail");
            base.OnDisappearing();
        }

        public void RemoveClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selected = button.BindingContext as string;
            var vm = BindingContext as LocationItemScanViewModel;
            vm.RemoveCommand.Execute(selected);
        }

        protected override void OnBindingContextChanged()
        {
            MyEditor.Focus();
            base.OnBindingContextChanged();
        }
    }
}