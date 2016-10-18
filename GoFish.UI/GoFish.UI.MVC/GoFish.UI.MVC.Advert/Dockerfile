FROM microsoft/dotnet:core
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app

EXPOSE 5003
ENV ASPNETCORE_URLS="http://0.0.0.0:5003"
ENTRYPOINT dotnet /app/GoFish.UI.MVC.Advert.dll