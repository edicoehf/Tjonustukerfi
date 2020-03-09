using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Handtolva.Tests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .InstalledApp("com.companyname.handtolvuapp")
                    .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}

//.ApkFile("../Xamarin.Android/Cache/Mono.Android.Platform.ApiLevel_28.apk")