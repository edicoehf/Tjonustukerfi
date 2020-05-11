# <p align="center">ServiceSystem Edico Bakend</p>
Xamarin application for the service system created by Edico.<br />
### <p>Dependencies</p>
NETstandard.Library 2.0.3 or greater

## <p align="center">How to run</p>
* Install NETstandard.Library
* Recomend to install Visual Studio 2019
    * When installing Visual Studio or when modifying a version you already have select the following:
        * Mobile delopment with .NET
        * .NET Core cross-platform development
* Clone this repository
* Restore NuGet packages for the solution
* Open Constant.cs file and update the connection URL
* If using other API then ThjonustukerfiWebAPI or setting up for a new customer do the following:
    * Make sure the models correspond to the modal delivered from API
    * Make sure the JSON files for each Modal correspond to the Json delivered from API
* Archive the HandtolvuApp.Android
* Transfer the .apk to the device and install<br />
Now the program is up an running but needs to be archived after each change before deployment<br />

## <p align="center">Packages</p>
NETStandard.Library --version: 2.0.3<br />
Newtonsoft.Json --version: 12.0.3<br />
Polly --version: 7.2.1<br />
Xam.Plugin.Connectivity --version: 3.2.0<br />
Xamarin.Essentials --version: 1.5.3.1<br />
Xamarin.Forms --version: 4.6.0.726<br />