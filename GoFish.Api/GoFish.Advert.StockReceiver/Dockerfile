FROM microsoft/dotnet:core
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

ENTRYPOINT dotnet /app/GoFish.Advert.StockReceiver.dll