FROM microsoft/dotnet:core
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

EXPOSE 5000
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
ENTRYPOINT dotnet /app/GoFish.Identity.dll