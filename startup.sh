#! /bin/bash
echo 'building datbase'
docker pull mcr.microsoft.com/mssql/server:2022-latest
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Passw0rd123" -p 1433:1433 --name mssql --hostname local-mssql --platform linux/amd64 -d mcr.microsoft.com/mssql/server:2022-latest
dotnet kentico-xperience-dbmanager -- -s "localhost" -d "SampleApp" -u "SA" -a "Passw0rd123" -p "Passw0rd123" --license-file "./license.txt" --hash-string-salt "3ead0e85-df41-4ad0-bd64-37c7422eb28e"
echo 'restoreing database'
dotnet run --kxp-ci-restore
echo 'done'