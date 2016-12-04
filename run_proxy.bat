@echo off
setlocal
cd src\server\Microservices\HttpGateway\HttpGatewayApp
dotnet restore
dotnet build
dotnet run

