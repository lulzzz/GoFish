FROM microsoft/dotnet:latest
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

EXPOSE 5003/tcp
ENV ASPNETCORE_URLS http://*:5003
ENTRYPOINT dotnet /app/GoFish.UI.MVC.dll