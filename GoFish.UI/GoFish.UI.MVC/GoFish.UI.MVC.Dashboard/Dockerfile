FROM microsoft/dotnet:core
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

EXPOSE 5005
ENTRYPOINT dotnet /app/GoFish.UI.MVC.Dashboard.dll