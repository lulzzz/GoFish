# Go Fish

## Fish Marketing Application

This web-application gives fishermen and fish merchants easy
access to a market of buyers including

- Individuals
- The wholesale market
- Restaurants

## Technology stack

- MS .NET Core (C#)
- EF Core Migrations
- Sqlite DB
- RabbitMQ

## Deployment options

1. There is a Vagrantfile in the root that will spin up a VM with the prerequisites.
    - OS - Currently Ubuntu
    - RabbitMQ instance
    - Docker

    Once you have Vagant (and virtulbox) installed,  run `vagrant up` from the root of the project

1. You can install RabbitMQ on your box, and run the code from VS

## Roadmap

1. Deploy MicroServices into our Docker environment
1. Create suite of tests for the API
1. Use EventStore for order processing
1. Create web-based UI for services in ReactJs or Angular2