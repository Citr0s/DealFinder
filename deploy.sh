#!/bin/sh

GREEN='\033[0;32m' # Green
NC='\033[0m' # No Color

echo -e "${GREEN}*** Killing dotnet process... ***${NC}";
pkill -HUP dotnet

echo -e "${GREEN}*** Getting latest changes from git... ***${NC}";
git pull

echo -e "${GREEN}*** Changing directory to Web ***${NC}";
cd DealFinder.Web/

echo -e "${GREEN}*** Updating packages... ***${NC}";
yarn

echo -e "${GREEN}*** Compiling angular files... ***${NC}";
ng build --prod

echo -e "${GREEN}*** Changing directory to Api ***${NC}";
cd ../DealFinder.Api/

echo -e "${GREEN}*** Compiling dotnet files... ***${NC}";
dotnet restore

echo -e "${GREEN}*** Starting dotnet process... ***${NC}";
dotnet run > stdout.txt 2> stderr.txt &
