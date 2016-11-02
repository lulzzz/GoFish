FROM microsoft/dotnet:1.1.0-preview1-runtime
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS="http://0.0.0.0:5003"
EXPOSE 5003
COPY ./bin/release/netcoreapp1.1/publish/ /app
WORKDIR /app
ENTRYPOINT dotnet /app/GoFish.UI.MVC.Advert.dll