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

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<OrderInputViewModel, string>(this, "NoOrder");
            base.OnDisappearing();
        }
    }
}