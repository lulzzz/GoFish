FROM microsoft/dotnet:latest
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

EXPOSE 5000
ENTRYPOINT dotnet /app/GoFish.Identity.dll