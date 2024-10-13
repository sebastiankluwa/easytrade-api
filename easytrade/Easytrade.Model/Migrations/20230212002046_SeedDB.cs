using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Easytrade.Model.Migrations
{
    public partial class SeedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "Allocation", "DayProfit", "IsActive", "MaxOpenPositions", "MinimumAllocation", "Name", "Symbols", "TotalProfit" },
                values: new object[] { 1L, 100m, 0m, true, 5, 10m, "MACD Bot", new[] { "BTCUSDT" }, 0m });

            migrationBuilder.InsertData(
                table: "BuyOrders",
                columns: new[] { "Id", "Amount", "BotId", "Fee", "OrderDate", "Pair", "ProfitLossId", "Rate", "ReferenceOrderId", "Side", "Status" },
                values: new object[,]
                {
                    { 1L, 10m, 1L, 0.5m, new DateTime(2022, 12, 1, 13, 1, 1, 0, DateTimeKind.Utc), "BTCUSDT", null, 10000m, 1L, "Buy", 0 },
                    { 2L, 20m, 1L, 1m, new DateTime(2022, 12, 3, 13, 1, 1, 0, DateTimeKind.Utc), "BTCUSDT", null, 1000m, 2L, "Buy", 0 }
                });

            migrationBuilder.InsertData(
                table: "SellOrders",
                columns: new[] { "Id", "Amount", "BotId", "Fee", "OrderDate", "Pair", "ProfitLossId", "Rate", "ReferenceOrderId", "Side", "Status" },
                values: new object[,]
                {
                    { 1L, 10m, 1L, 0.5m, new DateTime(2022, 12, 2, 13, 1, 1, 0, DateTimeKind.Utc), "BTCUSDT", 1L, 11000m, 1L, "Sell", 0 },
                    { 2L, 20m, 1L, 1m, new DateTime(2022, 12, 4, 13, 1, 1, 0, DateTimeKind.Utc), "BTCUSDT", 2L, 1100m, 2L, "Sell", 0 }
                });

            migrationBuilder.InsertData(
                table: "ProfitsLosses",
                columns: new[] { "Id", "BuyOrderId", "CompletionDate", "Result", "SellOrderId" },
                values: new object[] { 1L, 1L, null, 10000m, 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BuyOrders",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ProfitsLosses",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "SellOrders",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "BuyOrders",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "SellOrders",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
