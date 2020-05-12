using HandtolvuApp.Data;
using HandtolvuApp.Data.Implementations;
using HandtolvuApp.Data.Interfaces;
using HandtolvuApp.Data.Managers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HandtolvuApp
{
    public partial class App : Application
    {
        /// <summary>
        ///     Manager to handle all server calls for item
        /// </summary>
        public static ItemManager ItemManager { get; private set; }

        /// <summary>
        ///     Manager to handle all server calls for order
        /// </summary>
        public static OrderManager OrderManager { get; private set; }

        /// <summary>
        ///     Manager to handl all server calls for other info
        /// </summary>
        public static InfoManager InfoManager { get; private set; }

        /// <summary>
        ///     Used to reference device scanner
        /// </summary>
        public static IScanner Scanner;
        public App()
        {
            InitializeComponent();
            ItemManager = new ItemManager(new ItemService());
            OrderManager = new OrderManager(new OrderService());
            InfoManager = new InfoManager(new ItemService());
            GetStateAndLocations();
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

        /// <summary>
        ///     Used to load all states/locations on startup if device is connected
        /// </summary>
        private async void GetStateAndLocations()
        {
            await InfoManager.GetStateAndLocations();
        }
    }
}
