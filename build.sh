#!/bin/sh
case $1 in
    --publish-only) publishonly="true";;
    *) break;;
esac;

if [ "$publishonly" = "true" ]; then
    echo "publish-only flag set"
else
    echo "Restoring .NET Dependencies"
    dotnet restore

    echo "Building Application"
    dotnet build ./**/project.json
fi

if [ "$publishonly" = "true" ]; then
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

echo "Precompiling Razor Views"
cd ./GoFish.UI/GoFish.UI.MVC/GoFish.UI.MVC.Dashboard
dotnet razor-precompile -c release -f netcoreapp1.1 -o ./bin/release/netcoreapp1.1/publish
cd ../GoFish.UI.MVC.Advert
dotnet razor-precompile -c release -f netcoreapp1.1 -o ./bin/release/netcoreapp1.1/publish
cd ../GoFish.UI.MVC.Inventory
dotnet razor-precompile -c release -f netcoreapp1.1 -o ./bin/release/netcoreapp1.1/publish
cd ../../../GoFish.Identity
dotnet razor-precompile -c release -f netcoreapp1.1 -o ./bin/release/netcoreapp1.1/publish
cd ../

echo "running tests"
cd ./GoFish.Api/GoFish.Advert.tests
dotnet test