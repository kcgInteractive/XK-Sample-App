### Command to initialize a database

Replace with your credentials

`dotnet kentico-xperience-dbmanager -- -s "xperience-dev-db.cq4bh95s90ww.us-east-2.rds.amazonaws.com" -d "Designo" -u "admin" -a "MyStr0ngPa$$word" -p "MyStr0ngPa$$word" --license-file "./license.txt"`

[Database Manager Reference](https://docs.xperience.io/xp/developers-and-admins/installation)

### Commands to start local database instance if one is not created

Replace with your credentials and make sure to have Docker installed

`sudo docker pull mcr.microsoft.com/mssql/server:2022-latest`

```
sudo docker run -e "ACCEPT_EULA=Y" \
   -e "MSSQL_SA_PASSWORD=Passw0rd" \
   -p 1433:1433 \
   --name mssql \
   --hostname local-mssql \
   --platform linux/amd64 \
   -d mcr.microsoft.com/mssql/server:2022-latest
```

### Commands to generate code files from Admin Interface

`dotnet run --no-build -- --kxp-codegen --type "All"`

[Codegen Reference](https://docs.xperience.io/xp/developers-and-admins/api/generate-code-files-for-system-objects)

### [Widget Properties Reference](https://docs.xperience.io/xp/developers-and-admins/customization/extend-the-administration-interface/ui-form-components/reference-admin-ui-form-components)

### [Continuous Deployment Reference](https://docs.xperience.io/xp/developers-and-admins/ci-cd/continuous-deployment)

### Command to generate CD configuration file

`dotnet run --no-build -- --kxp-cd-config --path "C:\CD_configs\Production\repository.config"`

### Command to add retrieve sync files from database

`dotnet run --no-build --kxp-ci-store`

### Command to restore sync files to database

`dotnet run -- --kxp-cd-restore --repository-path "C:\Xperience_Deployments\Production\CDRepository"`
