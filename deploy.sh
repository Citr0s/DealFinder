#!/bin/sh

GREEN='\033[0;32m' # Green
NC='\033[0m' # No Color

echo '${GREEN}*** Killing dotnet process... ***${NC}';
pkill -HUP dotnet

echo '${GREEN}*** Getting latest changes from git... ***${NC}';
git pull

echo '${GREEN}*** Changing directory to Web ***${NC}';
cd DealFinder.Web/

echo '${GREEN}*** Updating packages... ***${NC}';
yarn

echo '${GREEN}*** Compiling angular files... ***${NC}';
ng build --prod

echo '${GREEN}*** Changing directory to Api ***${NC}';
cd ../DealFinder.Api/

echo '${GREEN}*** Compiling dotnet files... ***${NC}';
dotnet restore

echo '${GREEN}*** Starting dotnet process... ***${NC}';
dotnet run > stdout.txt 2> stderr.txt &
