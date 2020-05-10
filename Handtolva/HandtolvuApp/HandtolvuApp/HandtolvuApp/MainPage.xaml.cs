using HandtolvuApp.FailRequestHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HandtolvuApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if(FailedRequstCollection.ItemFailedRequests.Count > 0)
            {
                FailedRequst.IsVisible = true;
            }
            else
            {
                FailedRequst.IsVisible = false;
            }
            base.OnAppearing();
        }
    }
}
