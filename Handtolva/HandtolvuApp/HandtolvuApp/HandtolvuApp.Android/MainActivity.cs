using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using HandtolvuApp.Droid.Services;
using Symbol.XamarinEMDK;

namespace HandtolvuApp.Droid
{
    [Activity(Label = "HandtolvuApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public ScannerService Scanner { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Scanner = DependencyService.Get<ScannerService>();
            EMDKManager.GetEMDKManager(Android.App.Application.Context, Scanner);
            App.Scanner = Scanner;
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Scanner.InitScanner();
        }

        protected override void OnPause()
        {
            base.OnPause();
            Scanner.DeinitScanner();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Scanner.Destroy();
        }
    }
}