using HandtolvuApp.Data;
using HandtolvuApp.Data.Implementations;
using HandtolvuApp.Data.Managers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandtolvuApp
{
    public partial class App : Application
    {
        public static ItemManager ItemManager { get; private set; }
        public static OrderManager OrderManager { get; private set; }
        public App()
        {
            InitializeComponent();
            ItemManager = new ItemManager(new ItemService());
            OrderManager = new OrderManager(new OrderService());
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
