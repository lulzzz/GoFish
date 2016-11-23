FROM microsoft/dotnet:core
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS="http://0.0.0.0:5001"
EXPOSE 5001
COPY ./bin/release/netcoreapp1.1/publish/ /app
WORKDIR /app
ENTRYPOINT dotnet /app/GoFish.Advert.dll