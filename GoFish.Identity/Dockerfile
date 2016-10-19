FROM microsoft/dotnet:core
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
EXPOSE 5000
COPY ./bin/release/netcoreapp1.0/publish/ /app
WORKDIR /app
ENTRYPOINT dotnet /app/GoFish.Identity.dll