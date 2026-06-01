# API FINISHED

## About the api
This API was developed with the purpose to be able to create different tasks for a single company, with the only next status options:
1. Pending 
2. In Progress
3. Completed
4. Cancelled

**BUT**, I tried to make something different. I tried to designed a more scalable and personalized system. So I added the extra functinoality to the API to be able to create different companies, and each company can create their own status.

## What do I used on this api
* EF
* JWT Tokens authorization
* DTO query params
* DTO classes for responses
* Response Standard Format for endpoints
* EF navigation property

## Instructions to use the api

On this proyect I used the .net 8.0 version. **You should change the version from the commands to yout .net version**
1. Install .net Entity Framework
    1. dotnet add package Microsoft.EntityFrameworkCore --version 8.0.8

2. Install Pomelo, for using EF with MYSQL
    1. dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.2 

3. Set a secret to configure the database connection, changing the iformation as the following example:
    1. dotnet user-secrets init
    2. dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;port=xxxx;database=MyDbName;user=myuser;password=1234"

4. For saving the passwords on the database in a encryptated way
    1. dotnet add package BCrypt.Net-Next --version 4.2.0

5. Using JWT authentication
    1. dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.0

6. Creating the JWT
    1. dotnet add package System.IdentityModel.Tokens.Jwt --version 8.0.0

7. Execute the queryes on folder DB_Description to create the database and the tables
