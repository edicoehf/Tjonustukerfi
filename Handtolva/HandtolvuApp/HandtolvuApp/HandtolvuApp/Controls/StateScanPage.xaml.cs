using HandtolvuApp.Data.Interfaces;
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
    public partial class StateScanPage : ContentPage
    {
        public StateScanPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<StateScanViewModel, string>(this, "Success", async (sender, message) =>
            {
               // DependencyService.Get<IToast>().Show("Success!");
                await App.Current.MainPage.DisplayAlert("Klárað!", message, "OK");
                MyEditor.Focus();
            });

            MessagingCenter.Subscribe<StateScanViewModel, string>(this, "Fail", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa!", message, "OK");
                MyEditor.Focus();
            });

            MyEditor.Focus();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<StateScanViewModel, string>(this, "Success");
            MessagingCenter.Unsubscribe<StateScanViewModel, string>(this, "Fail");
            base.OnDisappearing();
        }
    }
}