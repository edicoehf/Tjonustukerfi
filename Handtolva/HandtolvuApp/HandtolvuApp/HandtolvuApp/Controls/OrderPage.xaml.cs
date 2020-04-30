using HandtolvuApp.Data.Implementations;
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
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<OrderService>(this, "Success", async (sender) =>
            {
                await App.Current.MainPage.DisplayAlert("Klárað", $"Pöntun hefur verið skráð sótt", "Ok");
            });
        }

        protected override void OnAppearing()
        {
            MyCollectionView.SelectedItem = null;
            base.OnAppearing();
        }
    }
}