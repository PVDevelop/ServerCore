@echo off
call install_npm
call build_npm
call dotnet
start run_auth
start run_proxy
