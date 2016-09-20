FROM microsoft/dotnet:latest
COPY ./bin/Debug/netcoreapp1.0/publish/ /app
WORKDIR /app

ENTRYPOINT dotnet /app/GoFish.Advert.Receiver.dll