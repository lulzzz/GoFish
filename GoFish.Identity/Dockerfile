FROM microsoft/dotnet:1.1.0-preview1-runtime
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
EXPOSE 5000
COPY ./bin/release/netcoreapp1.1/publish/ /app
WORKDIR /app
ENTRYPOINT dotnet /app/GoFish.Identity.dll