# easytrade-backend

This repository contains the backend API for a cryptocurrency investment web application, designed to support investing on the Binance exchange. The API facilitates the automated management of investment robots and provides market analysis tools, integrating investment algorithms, real-time data, and charting functionalities for technical analysis.

## Key Features

- **Automated Trading Bots**: Create and manage investment robots that automatically trade cryptocurrencies based on signals from custom investment algorithms.
- **Market Analysis**: Tools and charts for technical analysis of cryptocurrency markets.
- **Integration with Binance**: Provides access to cryptocurrency data and trading on Binance exchange.
  
## Tech Stack

- **Backend**: Built with `.NET 6`, a powerful framework for high-performance web applications in C#.
- **Database**: PostgreSQL is used to securely manage application data. The database runs in an isolated environment using Docker containers.

## Usage

**Create EF migration from command line**

```
dotnet ef migrations add InitialCreate --context EasyTradeDbContext -s ..\Easytrade.Api\Easytrade.Api.csproj
```


- The **-s** parameter indicates the startup project with connection string  `-s ..\DataMatrix.Api\DataMatrix.Api.csproj`


**Update DB from command line**

```
dotnet ef database update --context EasytradeDbContext -s ..\Easytrade.Api\Easytrade.Api.csproj
```
