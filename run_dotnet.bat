@echo off
setlocal
cd src\server
call dotnet restore
cd ..\..
start run_proxy
start run_auth