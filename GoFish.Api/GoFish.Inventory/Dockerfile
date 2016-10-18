FROM microsoft/dotnet:core
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

EXPOSE 5002
ENV ASPNETCORE_URLS="http://0.0.0.0:5002"
ENTRYPOINT dotnet /app/GoFish.Inventory.dll