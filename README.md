React Vefur:  
[![Build Status](https://dev.azure.com/edicoehf/Tjonustukerfi/_apis/build/status/React%20Vefur%20Pipeline?branchName=master)](https://dev.azure.com/edicoehf/Tjonustukerfi/_build/latest?definitionId=8&branchName=master)

.Net Core Bakendi:  
[![Build Status](https://dev.azure.com/edicoehf/Tjonustukerfi/_apis/build/status/.Net%20Bakendi%20Pipeline?branchName=master)](https://dev.azure.com/edicoehf/Tjonustukerfi/_build/latest?definitionId=7&branchName=master)
# Þjónustukerfi Edico ehf.
Lokaverkefni gert af nemendum úr Háskólanum í Reykjavík

# Setup
## React-app and dotnet Webapi
This system uses docker and docker-compose for setup. The host must support Docker and Docker-compose. The database connection to postgres, the smtp email service and the connection to the WebApi for the React-App are setup and used through environment variables that docker-compose reads from a .env file. Docker-compose runs each part of this system in seperate containers but on the same network (thjonustukerfi-network). The WebApi and react-app are accessible outside of the docker-network on their exposed ports but the postgres database is closed off, the WebApi is the only one that has access to the database. <br />

Each company that uses the system has a config json file that is located in Tjonustukerfi\Bakendi\ThjonustukerfiWebAPI\Config. This should be set up with regards to the company's need. To choose which config the application loads up, head to program.cs and follow the instructions there.

This system has the following three containers:
* WebApi (dotnet)
* Database (Postgres)
* React-app (node)

The system is setup to work when docker-compose is spun up, given that the environmental variables and company config are set up. <br />
**Note:** Before doing the initial run make sure that the WebApi has the initial migrations in the migrations folder in the web api project. See the [web api documentation](https://github.com/edicoehf/Tjonustukerfi/tree/master/Bakendi) for more details on how to do that. <br />

The (default) open ports to the docker network are as follows:
* Port 5000 is mapped to the WebApi, but can be changed in the .env file (API_PORT)
* Port 80 is mapped to the React-app
<br />

**Environment variables need to be set in a .env file at the same place as docker-compose.yml** <br />
The environment files that need to be set are:<br />
* postgresUser=&#60;value&#62;
* postgresPassword=&#60;value&#62;
* postgresDatabase=&#60;value&#62;
* SMTP_USERNAME=&#60;value&#62;
* SMTP_PASSWORD=&#60;value&#62;
* SMTP_SERVER=&#60;value&#62;
* SMTP_PORT=&#60;value&#62;
* API_URL=&#60;value&#62; - (this is the Url/IP of the host computer)
* API_PORT=&#60;value&#62;

To run the system you have to do the following:
* Setup Docker (if not already set up)
* Setup Docker-compose (if not already set up)
* Setup the .env file (if not already set up)
* Setup the company's config settings (if not already set up)
* In the terminal where the docker-compose.yml is located run:
    * docker-compose up
* To run in detached mode run:
    * docker-compose up -d

How to turn off docker-compose:
* If the program was run in detached mode run:
    * docker-compose down
* If not in detached mode use ctrl + c

# Running in development
## WebApi
See the README.md in the folder Bakendi: <br />
https://github.com/edicoehf/Tjonustukerfi/tree/master/Bakendi

## React-app
See the README.md in the folder thjonustukerfi-react-app: <br />
https://github.com/edicoehf/Tjonustukerfi/tree/master/Vefur/thjonustukerfi-react-app

## Xamarin application
See the README.md in the folder Handtolva: <br />
https://github.com/edicoehf/Tjonustukerfi/tree/master/Handtolva
