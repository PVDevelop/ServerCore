@echo off
setlocal
cd src\server\Microservices\Authentication\AuthenticationApp
call dotnet build
call dotnet run