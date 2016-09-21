FROM microsoft/dotnet:latest
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

ENTRYPOINT dotnet /app/GoFish.Advert.Receiver.dll