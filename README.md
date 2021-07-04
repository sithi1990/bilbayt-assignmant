# Bilbayt Assignment

This application is developed by following technologies:

- Used Clean Architecture to develop entire backend application. 
- Frontend SPA application is developed by Angular 7.
- Backend is developed by ASP.NET Core 3.1 and .NET Standard 2.0 class libraries.
- Used NSwag to generate API documentation and used NSwag.MSBuild package to automatically generate TypeScript HttpClient proxies.
- Used CosmosDB as the storage.
- Used SendGrid to send email notifications.
- Used xUnit and Moq to write unit tests.

### Prerequisites
1. Visual Studio 2019.
2. NodeJS v10.15.3 or Higher.
3. CosmosDB emulator - [(Download)](https://cosmosdbportalstorage.azureedge.net/emulator/2021_06_17_2.14.1-08dca53e/azure-cosmosdb-emulator-2.14.1-08dca53e.msi)

### How to Debug locally

1. Install CosmosDB emulator and start emulator.
2. Run "npm install" command from "Assignment.Web/ClientApp" folder.
3. Set "Assignment.Web" project as startup project. 
4. Run the application code in Debug mode.
