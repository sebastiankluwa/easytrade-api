using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Easytrade.Model.Migrations
{
    public partial class MoreSeedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Allocation", "TotalProfit" },
                values: new object[] { 1000m, 10000m });

            migrationBuilder.InsertData(
                table: "Bots",
                columns: new[] { "Id", "Allocation", "DayProfit", "IsActive", "MaxOpenPositions", "MinimumAllocation", "Name", "Symbols", "TotalProfit" },
                values: new object[,]
                {
                    { 2L, 2000m, 0m, true, 2, 5m, "RSI Bot", new[] { "ETHUSDT" }, 0m },
                    { 3L, 75m, 0m, false, 3, 7.5m, "EMA Bot", new[] { "LTCUSDT" }, 0m }
                });

            migrationBuilder.UpdateData(
                table: "BuyOrders",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ProfitLossId", "Status" },
                values: new object[] { 1L, 1 });

            migrationBuilder.UpdateData(
                table: "BuyOrders",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ProfitLossId", "Status" },
                values: new object[] { 2L, 1 });

            migrationBuilder.InsertData(
                table: "ProfitsLosses",
                columns: new[] { "Id", "BuyOrderId", "CompletionDate", "Result", "SellOrderId" },
                values: new object[] { 2L, 2L, null, 2000m, 2L });

            migrationBuilder.UpdateData(
                table: "SellOrders",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SellOrders",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Status",
                value: 1);

            migrationBuilder.InsertData(
                table: "BuyOrders",
                columns: new[] { "Id", "Amount", "BotId", "Fee", "OrderDate", "Pair", "ProfitLossId", "Rate", "ReferenceOrderId", "Side", "Status" },
                values: new object[,]
                {
                    { 3L, 30m, 2L, 1.5m, new DateTime(2022, 12, 5, 13, 1, 1, 0, DateTimeKind.Utc), "ETHUSDT", 3L, 400m, 3L, "Buy", 1 },
                    { 4L, 40m, 2L, 2m, new DateTime(2022, 12, 7, 13, 1, 1, 0, DateTimeKind.Utc), "ETHUSDT", null, 200m, 4L, "Buy", 0 }
                });

            migrationBuilder.InsertData(
                table: "SellOrders",
                columns: new[] { "Id", "Amount", "BotId", "Fee", "OrderDate", "Pair", "ProfitLossId", "Rate", "ReferenceOrderId", "Side", "Status" },
                values: new object[] { 3L, 30m, 3L, 2m, new DateTime(2022, 12, 6, 13, 1, 1, 0, DateTimeKind.Utc), "ETHUSDT", 2L, 500m, 2L, "Sell", 1 });

            migrationBuilder.InsertData(
                table: "ProfitsLosses",
                columns: new[] { "Id", "BuyOrderId", "CompletionDate", "Result", "SellOrderId" },
                values: new object[] { 3L, 3L, null, 3000m, 3L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BuyOrders",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "ProfitsLosses",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ProfitsLosses",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "BuyOrders",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "SellOrders",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Allocation", "TotalProfit" },
                values: new object[] { 100m, 0m });

            migrationBuilder.UpdateData(
                table: "BuyOrders",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ProfitLossId", "Status" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "BuyOrders",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ProfitLossId", "Status" },
                values: new object[] { null, 0 });

            migrationBuilder.UpdateData(
                table: "SellOrders",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Status",
                value: 0);

            migrationBuilder.UpdateData(
                table: "SellOrders",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Status",
                value: 0);
        }
    }
}
