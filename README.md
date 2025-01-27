IntraApi: Indicates a focus on internal architecture and dependency management.

Instructions on how to build and run the application
Build and run the application:
In file folder you can find folder with name “_SQL”, here is the script with initial load of SQL
tables and some DUMMY data.
Open Microsoft SQL Server Management Studio, open a file “_SQL.sql”.
For this example, I was created DB [AcademyLink], some user and login “AcademyLinkUser”. This user has
rights to operate with the tables that are used in the API. (These credentials will be
necessary when we set up the connection string in the application.)

When everything is created in the DB, please open Microsoft Visual Studio Community
2022 (64-bit), navigate to the project and open the solution “SyncpWalletAPI.sln”.
Navigate to project “API/AcademyLinkUser.API” and find the file “appsettings.json”.
Here
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "AcademyLinkConnectionString": "server=(local);database=AcademyLink;User Id=AcademyLinkUser;password=WebAPIPassword;TrustServerCertificate=true;"
  },
  "ApiUrl": "https://localhost:7070"
}
