#!/bin/sh
dotnet publish -c release ./GoFish.Advert
dotnet publish -c release ./GoFish.Inventory
dotnet publish -c release ./GoFish.Advert.Receiver
dotnet publish -c release ./GoFish.Inventory.Receiver
dotnet publish -c release ./GoFish.Identity
dotnet publish -c release ./GoFish.UI.MVC