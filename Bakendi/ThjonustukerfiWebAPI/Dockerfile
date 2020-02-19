FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR app/

LABEL maintainer = "Edico ehf."

ENV ASPNETCORE_URL=http//+:5000
ENV ASPNETCORE_ENVIRONMENT Production

COPY . /app
WORKDIR /app
RUN dotnet publish -c Release
COPY /bin/Release/netcoreapp3.1/publish/ ./build

ENTRYPOINT ["dotnet", "build/ThjonustukerfiWebAPI.dll"]