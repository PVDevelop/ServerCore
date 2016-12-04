@echo off
setlocal
cd src\server\Microservices\HttpGateway\HttpGatewayApp
call dotnet build
call dotnet run