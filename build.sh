#!/bin/sh
while [[ "$#" > 1 ]]; do case $1 in
    --publish-only) publishonly="$2";;
    --skip-bower) skipbower="$2";;
    *) break;;
  esac; shift; shift
done

if [ "$publishonly" = "true" ]; then
    echo "publish-only flag set"
else
    echo "Restoring .NET Dependencies"
    dotnet restore

    echo "Building Application"
    dotnet build ./**/project.json
fi

if [ "$skipbower" = "true" ]; then
    echo "Bower install skipped"
else
    echo "Restoring Bower Dependencies..."
    cd ./GoFish.Identity/
    bower install

    cd ../GoFish.UI/GoFish.UI.MVC/GoFish.UI.MVC.Advert
    bower install

    cd ../GoFish.UI.MVC.Inventory
    bower install

    cd ../GoFish.UI.MVC.Dashboard
    bower install

    cd ../../../
fi

echo  "Publishing application"
dotnet publish -c release ./GoFish.Api/GoFish.Advert
dotnet publish -c release ./GoFish.Api/GoFish.Inventory
dotnet publish -c release ./GoFish.Api/GoFish.Advert.Receiver
dotnet publish -c release ./GoFish.Api/GoFish.Inventory.Receiver
dotnet publish -c release ./GoFish.Identity
dotnet publish -c release ./GoFish.UI/GoFish.UI.MVC/GoFish.UI.MVC.Dashboard
dotnet publish -c release ./GoFish.UI/GoFish.UI.MVC/GoFish.UI.MVC.Advert
dotnet publish -c release ./GoFish.UI/GoFish.UI.MVC/GoFish.UI.MVC.Inventory