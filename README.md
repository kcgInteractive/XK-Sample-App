## Command to initialize a database (local/stage)

Replace with your credentials </br>

Local: <br>
`dotnet kentico-xperience-dbmanager -- -s "localhost" -d "SampleApp" -u "SA" -a "Passw0rd123" -p "Passw0rd123" --license-file "./license.txt" --hash-string-salt "3ead0e85-df41-4ad0-bd64-37c7422eb28e" --recreate-existing-database`

Stage:<br>
`dotnet kentico-xperience-dbmanager -- -s "xk-database-stage-express.cq4bh95s90ww.us-east-2.rds.amazonaws.com" -d "SampleApp" -u "admin" -a "Passw0rd123" -p "Passw0rd123" --license-file "./license-stage.txt" --hash-string-salt "3ead0e85-df41-4ad0-bd64-37c7422eb28e" --recreate-existing-database`

[Database Manager Reference](https://docs.xperience.io/xp/developers-and-admins/installation)

## Commands to start local database instance if one is not created

Replace with your credentials and make sure to have Docker installed<br>

`sudo docker pull mcr.microsoft.com/mssql/server:2022-latest`

```
sudo docker run -e "ACCEPT_EULA=Y" \
   -e "MSSQL_SA_PASSWORD=Passw0rd123" \
   -p 1433:1433 \
   --name mssql \
   --hostname local-mssql \
   --platform linux/amd64 \
   -d mcr.microsoft.com/mssql/server:2022-latest
```

### Commands for CI (Dev Database Sync)

Store to code CIRepository<br>
`dotnet run --no-build --kxp-ci-store`

The action deserializes the objects stored in the CI repository and creates, overwrites or removes corresponding data in the database. Objects are restored to the database specified by the CMSConnectionString in the application's configuration file.<br>
`dotnet run --no-build --kxp-ci-restore`

### Commands for Cd (Deployment Sync)

Prepare a configuration file that determines which types of objects are included and which operations are performed when restoring the repository.<br>
`dotnet run --no-build -- --kxp-cd-config --path "./App_Data/CD_Configs/repository.config"`

Store all supported objects from the database to a CD repository<br>
`dotnet run --no-build -- --kxp-cd-store --repository-path "./App_Data/CDRepository" --config-path "./App_Data/CD_Configs/repository.config"`

Transfer objects from the CD repository to the database of your deployment target (Should be added to buildspec.yml)<br>
`dotnet run --kxp-cd-restore --repository-path "./App_Data/CDRepository"`

[CI/CD Reference](https://docs.xperience.io/xp/developers-and-admins/ci-cd)
