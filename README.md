# easytrade-backend

**Create EF migration from command line**

```
dotnet ef migrations add InitialCreate --context EasyTradeDbContext -s ..\Easytrade.Api\Easytrade.Api.csproj
```


- The **-s** parameter indicates the startup project with connection string  `-s ..\DataMatrix.Api\DataMatrix.Api.csproj`


**Update DB from command line**

```
dotnet ef database update --context EasytradeDbContext -s ..\Easytrade.Api\Easytrade.Api.csproj
```
