# Go Fish

Seafood Marketing Application.

## Summary

A web-application giving fishermen and fish merchants easy
access to a market of buyers including:

- Individuals
- The wholesale market
- Restaurants

## Technology stack

- MS .NET Core 1.0.1 + WebApi (C#)
- EF Core 1.0.1 + Migrations
- Sqlite DB
- REDIS
- RabbitMQ
- Docker
- Vagrant & VirtualBox
- Ubuntu
- IdentityServer4
- EventStore

## Deployment options

### Prerequisites

To get up and running, ideally you will need:

- VirtualBox
- Vagrant

### Process

1. Clone the repo

    `git clone https://github.com/jakimber/GoFish`

1. Publish the application components:

    ```c#
    cd ./GoFish
    dotnet restore
    dotnet build ./**/project.json
    dotnet publish -c release ./GoFish.Advert
    dotnet publish -c release ./GoFish.Inventory
    dotnet publish -c release ./GoFish.Advert.Receiver
    dotnet publish -c release ./GoFish.Inventory.Receiver
    dotnet publish -c release ./GoFish.Identity
    dotnet publish -c release ./GoFish.UI.MVC
    ```

1. create the server (with docker-compose enabled):

    If required:
        `vagrant plugin install vagrant-docker-compose`

    and then: `vagrant up`

    This configures a virtual machine with:

    - Ubuntu OS
    - RabbitMQ instance
    - Docker
    - Docker Compose

    It also runs the Docker Compose file for the application and gets the application components running.

1. You will need to configure a new user in the RabbitMQ interface to match that in the code that uses it.  It is currently:

    `Username: gofish, Password: gofish`

1. You should now be able to use your browser to access the website at

    `http://localhost:8084`

1. You can debug the Api with Postman using the following settings in the authentication helper:


<img src="./Readme.Resources/postman-auth.png" alt="Drawing" style="width: 300px;margin-left:100px;"/>


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
1. Each service has it's own read-model datastore (currently Sqlite in all of them)
1. The Advert service's Events are published to EventStore to allow for event-sourcing
1. The Shopfront service may need splitting into smaller services

---

## Phase 1 roadmap

1. Ensure WebApi conforms to RMM level 4 to enable easy hypermedia navigation
1. Create web-based UI (ReactJs or Angular2)
1. Create suite of tests for the services

## Phase 2 feature list

- Reserving stock
- Buying stock
- Purchase Orders / Invoicing
- Payment Processing
- Shipping & Logistics
- Merchants - Fishermen sell to merchants who then communicate with punters on their behalf
- iOs / Android app consuming the same services (Xamarin?)
