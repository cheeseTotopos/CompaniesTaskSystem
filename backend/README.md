On this changes I started the first Controller, CompanyController.

Its necesary to install entity framework within backend/myapi folder. Use the command (change the version if you need)
    dotnet add package Microsoft.EntityFrameworkCore --version 8.0.8

For the mysql connection use the following command:
    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.2

For configuring the db connection, we use dotnet secrets:
    1.- First initialize dotnet secrets with the command
        dotnet user-secrets init

    2.- Set a secret with the database connection as the following example:
        dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;port=xxx;database=MyDb;user=myuser;password=1234" 