FROM microsoft/dotnet:latest
COPY ./bin/Release/netcoreapp1.0/publish/ /app
WORKDIR /app

ENTRYPOINT dotnet /app/GoFish.Advert.Receiver.dll