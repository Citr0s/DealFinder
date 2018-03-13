#!/bin/sh

echo 'Killing dotnet process...';
pkill -HUP dotnet

echo 'Getting latest changes from git...';
git pull

echo 'Changing directory to Web';
cd DealFinder.Web/

echo 'Updating packages...';
yarn

echo 'Compiling angular files...';
ng build --prod

echo 'Changing directory to Api';
cd ../DealFinder.Api/

echo 'Compiling dotnet files...';
dotnet restore

echo 'Starting dotnet process...';
dotnet run > stdout.txt 2> stderr.txt &
