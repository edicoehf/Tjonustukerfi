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
    public partial class LocationItemScanPage : ContentPage
    {
        public LocationItemScanPage()
        {
            InitializeComponent();
        }

        public void RemoveClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selected = button.BindingContext as string;
            var vm = BindingContext as LocationItemScanViewModel;
            vm.RemoveCommand.Execute(selected);
        }
    }
}