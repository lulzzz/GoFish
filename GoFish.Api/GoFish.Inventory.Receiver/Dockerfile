FROM microsoft/dotnet:core
COPY ./bin/release/netcoreapp1.1/publish/ /app
WORKDIR /app

ENTRYPOINT dotnet /app/GoFish.Inventory.Receiver.dll