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
            // Message that handles success requests
            MessagingCenter.Subscribe<StateScanViewModel, string>(this, "Success", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Klárað!", message, "OK");
                MyEditor.Focus();
            });

            // Message that handles failed requests
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
            // Unsubscribe from all messages
            MessagingCenter.Unsubscribe<StateScanViewModel, string>(this, "Success");
            MessagingCenter.Unsubscribe<StateScanViewModel, string>(this, "Fail");
            base.OnDisappearing();
        }
    }
}