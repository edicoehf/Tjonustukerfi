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

            MessagingCenter.Subscribe<ItemInputViewModel, string>(this, "Villa", async (sender, message) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa", message, "Ok");
                MyEditor.Focus();
            });

            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<ItemInputViewModel, string>(this, "Villa");
            base.OnDisappearing();
        }
    }
}