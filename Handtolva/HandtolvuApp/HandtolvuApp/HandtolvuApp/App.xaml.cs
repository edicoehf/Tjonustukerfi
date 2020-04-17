using HandtolvuApp.Data;
using HandtolvuApp.Data.Implementations;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandtolvuApp
{
    public partial class App : Application
    {
        public static ItemManager ItemManager { get; private set; }
        public App()
        {
            InitializeComponent();
            ItemManager = new ItemManager(new RestService(), new ItemService());
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
