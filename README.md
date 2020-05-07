React Vefur:  
[![Build Status](https://dev.azure.com/edicoehf/Tjonustukerfi/_apis/build/status/React%20Vefur%20Pipeline?branchName=master)](https://dev.azure.com/edicoehf/Tjonustukerfi/_build/latest?definitionId=8&branchName=master)

.Net Core Bakendi:  
[![Build Status](https://dev.azure.com/edicoehf/Tjonustukerfi/_apis/build/status/.Net%20Bakendi%20Pipeline?branchName=master)](https://dev.azure.com/edicoehf/Tjonustukerfi/_build/latest?definitionId=7&branchName=master)
# Þjónustukerfi Edico ehf.
Lokaverkefni gert af nemendum úr Háskólanum í Reykjavík

# Setup
## React-app and dotnet Webapi
This system uses docker and docker-compose for setup. The host must support Docker and Docker-compose. The database connection to postgres is used through environment variables that docker-compose reads from a .env file. Docker-compose runs each part of this system in seperate containers. <br />

This system has the following three containers:
* WebApi (dotnet)
* Database (Postgres)
* React-app (node)

The system is setup to work when docker-compose is spun up, given that the environmental variables are set up. <br />
Note: Before doing the initial run make sure that the webApi has the initial migrations setup in the migrations folder in the web api project. See the web api documentation for more details on how to do that. <br />

The open ports to the docker network are as follows:
* Port 5000 is mapped to the WebApi
* Port 80 is mapped to the React-app

To run the system you have to do the following:
* Setup Docker (if not already set up)
* Setup Docker-compose (if not already set up)
* In the terminal where the docker-compose.yml is located run:
    * docker-compose up
* To run in detached mode run:
    * docker-compose up -d

How to turn off docker-compose:
* If the program was run in detached mode run:
    * docker-compose down
* If not in detached mode use ctrl + c

# Running in production
## WebApi
See the README.md in the folder Bakendi: <br />
https://github.com/edicoehf/Tjonustukerfi/tree/master/Bakendi

## React-app
See the README.md in the folder thjonustukerfi-react-app: <br />
https://github.com/edicoehf/Tjonustukerfi/tree/master/Vefur/thjonustukerfi-react-app

## Xamarin application
See the README.md in the folder Handtolva: <br />
https://github.com/edicoehf/Tjonustukerfi/tree/master/Handtolva