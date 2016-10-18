FROM microsoft/dotnet:core
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

EXPOSE 5001
ENV ASPNETCORE_URLS="http://0.0.0.0:5001"
ENTRYPOINT dotnet /app/GoFish.Advert.dll