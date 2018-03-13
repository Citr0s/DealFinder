#!/bin/sh

echo 'Killing dotnet process...';
pkill -HUP dotnet

echo 'Getting latest changes from git...';
git pull

echo 'Updating packages...';
cd DealFinder.Web/
yarn
ng build --prod

echo 'Compiling files...';
cd ../DealFinder.Api/
dotnet restore

echo 'Starting dotnet process...';
dotnet run > stdout.txt 2> stderr.txt &
