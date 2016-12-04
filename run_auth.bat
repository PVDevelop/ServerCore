@echo off
setlocal
cd src\server\Microservices\Authentication\AuthenticationApp
dotnet restore
dotnet build
dotnet run

