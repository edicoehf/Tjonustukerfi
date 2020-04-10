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
            MessagingCenter.Subscribe<ItemInputViewModel>(this, "Villa", async (sender) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa", "Vörunúmer er ekki til", "Ok");
            });
        }
    }
}