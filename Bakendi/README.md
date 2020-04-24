[![Build Status](https://dev.azure.com/edicoehf/Tjonustukerfi/_apis/build/status/.Net%20Bakendi%20Pipeline?branchName=master)](https://dev.azure.com/edicoehf/Tjonustukerfi/_build/latest?definitionId=7&branchName=master) <br />
# <p align="center">ServiceSystem Edico Bakend</p>
WebAPI for the service system created by Edico.<br />

### <p>SQL program</p>
Local postgres server for development.

### <p>Database type</p>
    -posgresql
### <p>How to add a table in the .NET framework with DataContext</p>
Add new table: <br />
    Add model/s to DataContext (public DbSet<className> TableName {get; set;}) <br />
    In terminal: <br />
        dotnet ef migrations add NameofTheTableYouAreAdding <br />
        dotnet ef database update (update the database witht the new table, might need to refresh postgres) <br />

Update table: <br />
    Change the models and then do the same as when adding a new table <br />

## <p align="center">Dependencies</p>
Dotnet SDK version 3.1.200 <br />
Postgres 10.10.x

## <p align="center">How to run</p>
* Install Dotnet SDK
* Install Postgres
* Clone/fork this repository
* Navigate to the ThjonustukerfiWebAPI folder
* Go to appsettings.json to change connection settings to your postgres database
* run following commands:
    * dotnet restore
    * dotnet ef migrations add InitMigration
    * dotnet ef database update
    * dotnet run

## <p align="center">Tests</p>
Test are run using MS tests.<br />
### How to run tests:<br />
* Navigate to the ThjonustukerfiTests folder
* Run: dotnet test

## <p align="center">Packages</p>
### <p>Web Api</p>
Automapper  - version: 9.0.0<br />
NpgSql.EntityFrameworkCore.Design --version: 3.1.1<br />
Newtonsoft.Json --version: 12.0.3<br />
Npgsql.EntityFrameworkCore.PostgreSQL --version: 2.1.0 <br />
Swashbuckle.AspNetCore --version: 5.2.1<br />
FluentScheduler --version: 5.3.0
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

## <p align="center">Database connection</p>
Local postgres server <br />
Go to appsettings.json to change connection settings

## <p align="center">API options</p>
All content type is: application/json

### <p>Documentation</p>
Run the web api and navigate to root and /swagger E.g.:
http://localhost:5000/swagger/index.html
