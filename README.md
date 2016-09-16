# Go Fish

Seafood Marketing Application.

## Summary

A web-application giving fishermen and fish merchants easy
access to a market of buyers including:

- Individuals
- The wholesale market
- Restaurants

## Technology stack

- MS .NET Core (C#) Web Api
- EF Core Migrations
- Sqlite DB
- RabbitMQ
- Docker
- Vagrant & VirtualBox
- Ubuntu

## Deployment options

1. There is a Vagrantfile in the root that will spin up a VM with the prerequisites.
    - Ubuntu OS
    - RabbitMQ instance
    - Docker

    Once you have Vagant (and virtulbox) installed,  run `vagrant up` from the root of the project

1. You can install RabbitMQ on your development box, and run the code from VS if you want, but the above is advised.

---

## Architecture

|GoFish.Advert|GoFish.Inventory|GoFish.Shopfront|
|:-:|:-:|:-:|
|_Advertise and Add Stock_|_List and Manage Stock_|_Browse Stock, User Profile, Reviews_|
|| _WebApi services containing context logic_
||||
||**Docker**|
|| _Hosts the above services_
||||
||**RabbitMQ**|
|| _Communicate between services_
||||
||**Ubuntu running on VirtualBox**|
|| _Docker for windows doesn't work on Windows 10 Home_
||||
||**Windows 10 Home**|
|| _Development box OS_
|

Notes:

1. The virtualbox instance can be spun up using the Vagrantfile in the repo.
1. Each service has it's own datastore (currently Sqlite in all of them)
1. The Shopfront service may need splitting into smaller services

---

## Phase 1 roadmap

1. Deploy services into our Docker environment
1. Create suite of tests for the services
1. Use EventStore for stock processing
1. Create web-based UI (ReactJs or Angular2)

## Phase 2 feature list

- Reserving stock
- Buying stock
- Purchase Orders / Invoicing
- Payment Processing
- Shipping & Logistics
- Merchants - Fishermen sell to merchants who then communicate with punters on their behalf
- iOs / Android app consuming the same services
