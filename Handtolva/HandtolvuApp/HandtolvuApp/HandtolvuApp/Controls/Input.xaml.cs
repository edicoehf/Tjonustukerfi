using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HandtolvuApp.Models;
using HandtolvuApp.ViewModels;

namespace HandtolvuApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Input : ContentView
    {
        public Input()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<InputViewModel>(this, "Villa", async (sender) =>
            {
                await App.Current.MainPage.DisplayAlert("Villa", "Vörunúmer er ekki til", "Ok");
            });
        }
    }
}