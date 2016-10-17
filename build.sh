#!/bin/sh
while [[ "$#" > 1 ]]; do case $1 in
    -o) options="$2";;
    *) break;;
  esac; shift; shift
done

if [ "$options" = "publish" ]; then
    echo "publish-only flag set"
else
    dotnet restore
    dotnet build ./**/project.json
fi

dotnet publish -c release ./GoFish.Api/GoFish.Advert
dotnet publish -c release ./GoFish.Api/GoFish.Inventory
dotnet publish -c release ./GoFish.Api/GoFish.Advert.Receiver
dotnet publish -c release ./GoFish.Api/GoFish.Inventory.Receiver
dotnet publish -c release ./GoFish.Identity
dotnet publish -c release ./GoFish.UI/GoFish.UI.MVC/GoFish.UI.MVC.Advert
dotnet publish -c release ./GoFish.UI/GoFish.UI.MVC/GoFish.UI.MVC.Dashboard