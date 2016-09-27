FROM microsoft/dotnet:latest
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

EXPOSE 5002/tcp
ENV ASPNETCORE_URLS http://*:5002
ENTRYPOINT dotnet /app/GoFish.Identity.dll