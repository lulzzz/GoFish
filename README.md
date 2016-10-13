# Go Fish

Seafood Marketing Application.

## Summary

A web-application giving fishermen and fish merchants easy
access to a market of buyers including:

- Individuals
- The wholesale market
- Restaurants

The live version of the API is published on an AWS EC2 Instance
here:  `http://54.171.92.206:5000/api/` see the API reference below for usage scenarios

## Technology stack

- MS .NET Core 1.0.1 + WebApi + MVC (C#)
- EF Core 1.0.1 + Migrations
- Sqlite DB
- REDIS
- RabbitMQ
- Docker & Docker-Compose
- Vagrant & VirtualBox
- Ubuntu
- IdentityServer4
- EventStore

## Developer Setup options

### Prerequisites

To get up and running, ideally you will need:

- VirtualBox
- Vagrant
- Bower

### Process

1. Clone the repo

    `git clone https://github.com/jakimber/GoFish`

1. Install the MVC client dependencies:

    ```c#
    cd ./GoFish/GoFish.UI.MVC/
    bower install
    ```

1. Publish the application components:

    If you are using a shell like Git-Bash you can simply:

    ```ssh
    ' in the root of GoFish there's this shell-script
    cd ../
    ./build.sh
    ```

    If you are using a command line that doesn't like shell scripts, open the `build.sh` file and run each of the lines listed

1. Create the server:

    If required:
        `vagrant plugin install vagrant-docker-compose`

    and then: `vagrant up`

    This configures a virtual machine with:

    - Ubuntu OS
    - RabbitMQ instance
    - Docker
    - Docker Compose

    It also then runs the Docker-Compose file for the application and gets the application components running.

1. You will need to configure a new user in the RabbitMQ interface to match that in the code that uses it.  It is currently:

    `Username: gofish, Password: gofish`

1. You should now be able to use your browser to access the website at `http://localhost:5003`

1. You can use and debug the Api at the following locations with Postman:

    `http://localhost:5001` - Adverts

    `http://localhost:5002` - Inventory

    using the following settings in the authentication helper:

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
1. The Advert service's Events are also published to an EventStore instance to allow for event-sourcing

---

## Phase 1 roadmap

1. Ensure WebApi conforms to RMM level 4 to enable easy hypermedia navigation
1. Create SPA web-based UI (ReactJs or Angular2)
1. Create suite of tests for the services
1. The Shopfront service will probably need splitting into smaller services

## Phase 2 feature list

- Reserving stock
- Buying stock
- Purchase Orders / Invoicing
- Payment Processing
- Shipping & Logistics
- Merchants - Fishermen sell to merchants who then communicate with punters on their behalf
- iOs / Android app consuming the same services (Xamarin?)

---

## API reference

_Note_  You will need to obtain a bearer token from the live instance to use this API.
Follow the Postman diagram above, exchanging the AuthUrl and AccessTokenUrl for the live instance.

- PUT /api/adverts/{id} - creates a new draft advert if it doesn't exist otherwise updates an advert (status=created)
- PUT /api/postedadverts/{id} - posts (submits) an advert
- PUT /api/publishedadverts/{id} - publishes an advert (mq does this automatically)
- GET /api/adverts - gets draft adverts
- GET /api/adverts/{id} - gets an existing advert
- GET /api/postedadverts - lists all adverts in "posted" state
- GET /api/publishedadverts - lists all adverts in "published" state
- GET /api/withdrawnadverts - lists all adverts in "withdrawn" state
- GET /api/postedadverts/{id} -- gets a posted advert
- GET /api/publishedadverts/{id} -- gets a published advert
- GET /api/adverts?status=active - created, posted, published
- GET /api/adverts?status=inactive - withdrawn, fulfilled
- DELETE / api/adverts/{id} - sets advert 1 status to withdrawn / fulfilled
