using BinanceApi.Client;
using BinanceApi.Client.Extensions;
using BinanceApi.Client.Impl;
using Easytrade.Api.StartupExtensions;
using Easytrade.Logic.Repositories;
using Easytrade.Logic.Repositories.Impl;
using Easytrade.Logic.Services;
using Easytrade.Logic.Services.Impl;
using Easytrade.Logic.Services.Strategies.Orders.PlaceOrder;
using Easytrade.Model.DbAccess;
using Easytrade.Model.Repositories;
using Easytrade.Model.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddBinanceServices(builder.Configuration);
builder.Services.AddScoped<IBinanceApiFacade, BinanceApiFacade>();

builder.Services.AddScoped<IBotRepository, BotRepository>();
builder.Services.AddScoped<IBuyOrderRepository, BuyOrderRepository>();
builder.Services.AddScoped<ISellOrderRepository, SellOrderRepository>();
builder.Services.AddScoped<ICompositeOrdersRepository, CompositeOrdersRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IPlaceOrderStrategyFactory, PlaceOrderStrategyFactory>();

builder.Services.AddDbContext<EasyTradeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("EasyTradeConnectionString") ?? string.Empty));

builder.Services.AddAutoMapperWithProfiles();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
