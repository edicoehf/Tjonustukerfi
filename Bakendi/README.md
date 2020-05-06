[![Build Status](https://dev.azure.com/edicoehf/Tjonustukerfi/_apis/build/status/.Net%20Bakendi%20Pipeline?branchName=master)](https://dev.azure.com/edicoehf/Tjonustukerfi/_build/latest?definitionId=7&branchName=master) <br />
# <p align="center">ServiceSystem Edico Bakend</p>
WebAPI for the service system created by Edico.<br />

### <p>SQL program</p>
Local postgres server for development. Postgres docker container for production.

### <p>Database type</p>
* Postgresql

### <p>Database connection</p>
Local postgres server <br />
Go to appsettings.json to change connection settings

## <p align="center">Dependencies</p>
Dotnet SDK version 3.1.200 <br />
Postgres 10.10.x

## <p align="center">How to run</p>
* Install Dotnet SDK
* Install Postgres
* Clone this repository
* Open appsettings.json to setup the database connection string
* Fill out the company config in Tjonustukerfi\Bakendi\ThjonustukerfiWebAPI\Config
* Set the company config name in Program.cs
* Make sure you are in the folder ThjonustukerfiWebAPI in the terminal and do the following:
    * dotnet restore
    * dotnet ef migrations add InitMigration
    * dotnet ef database update
    * dotnet run
After the this has been done once you will only need to do the "dotnet run" command. <br />

Note: If the project has migrations set up in the migrations folder you do not need to do these two steps when running the commands above as the program will migrate automatically:
* dotnet ef migrations add InitMigration
* dotnet ef database update

## <p>Migrations (adding new tables or updating old ones)</p>
Add new table: <br />
    Add model/s to DataContext (public DbSet<className> TableName {get; set;}) <br />
    In terminal: <br />
        dotnet ef migrations add NameofTheTableYouAreAdding <br />
        dotnet ef database update (update the database witht the new table, might need to refresh postgres) <br />

Update table: <br />
    Change the models and then do the same as when adding a new table <br />

## <p align="center">Tests</p>
Tests are run with MSTests. If the test class depends on another class (e.g. service using a repository) the other class is Mocked which mocks the classes functionality. Each test class is used to for one class in the project. The test classes are setup to configure variables and things needed before each test is run, they also do a cleanup after each test.<br />
### How to run tests:<br />
* Navigate to the ThjonustukerfiTests folder
* Run: dotnet test

## <p>Documentation</p>
For documentation we used Swagger to dockument this Web api. This helps devolopers working with the api see what it has to offer. It also gives a convenient overview of the functionality of the api. Having good documentation makes it easier for others to jump in and start using and editing the api. <br />
To look at the swagger documentaion do the following:
* Navigate to the ThjonustukerfiWebAPI folder in your terminal and execute: dotnet run
* Open a browser and go to: http://<host>:<port>/swagger
    * E.g. http://localhost:5000/swagger

## <p align="center">Packages</p>
### <p>Web Api</p>
Automapper --version: 9.0.0<br />
BarcodeLib --version: 2.2.5<br />
FluentScheduler --version: 5.3.0<br />
Mailkit --version: 2.6.0<br />
NpgSql.EntityFrameworkCore.Design --version: 3.1.1<br />
Microsoft.Extensions.Logging.Log4Net.AspNetCore --version: 3.1.0<br />
Microsoft.Windows.Compatibility --version: 3.1.0<br />
Newtonsoft.Json --version: 12.0.3<br />
Npgsql.EntityFrameworkCore.PostgreSQL --version: 2.1.0 <br />
Swashbuckle.AspNetCore --version: 5.2.1

### <p>Test Project</p>
coverlet.collector --version: 1.0.1<br />
Microsoft.EntityFrameworkCore.Design --version: 3.1.1<br />
Microsoft.EntityFrameworkCore.InMemory --version:3.1.2 <br />
Microsoft.NET.Test.Sdk --version: 16.2.0<br />
Moq --version: 4.13.1<br />
MSTest.TestAdapter --version: 2.0.0<br />
MSTest.TestFramework --version: 2.0.0<br />
nbuilder --version: 6.1.0<br />
nsubstitute --version: 4.2.1<br />