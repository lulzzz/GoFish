FROM microsoft/dotnet:core
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

EXPOSE 5003
ENTRYPOINT dotnet /app/GoFish.UI.MVC.dll