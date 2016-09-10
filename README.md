# go-Fish

## Fish Marketing Application

This web-application gives fishermen and fish merchants easy
access to a market of buyers from individuals to restaurants
to wholesalers.

## Technology stack

* MS .NET Core (C#)
* EF Core Migrations
* Sqlite DB

Currently this runs as a .NET Console application as proof
of concept work continues.

## Roadmap

1. Split Bounded Contexts into MicroServices
1. Deploy MicroServices into a Docker environment
1. Use a Message Queue to choreograph services
1. Create suite of tests for the API
1. Use EventStore for order processing
1. Create web-based UI for services in ReactJs or Angular2