FROM microsoft/dotnet:latest
COPY ./bin/Debug/netcoreapp1.0/publish/ /app
WORKDIR /app

EXPOSE 5001/tcp
ENV ASPNETCORE_URLS http://*:5001
ENTRYPOINT dotnet /app/GoFish.Inventory.dll