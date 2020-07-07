FROM mcr.microsoft.com/dotnet/core/sdk:3.1

LABEL maintainer = "Edico ehf."

ENV ASPNETCORE_URL=http//+:5000
ENV ASPNETCORE_ENVIRONMENT Production

COPY . /app
WORKDIR /app
RUN apt-get update && apt-get install -y libgdiplus
RUN dotnet publish -c Release

# DABS change:
# ENTRYPOINT ["dotnet", "bin/Release/netcoreapp3.1/publish/ThjonustukerfiWebAPI.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet bin/Release/netcoreapp3.1/publish/ThjonustukerfiWebAPI.dll