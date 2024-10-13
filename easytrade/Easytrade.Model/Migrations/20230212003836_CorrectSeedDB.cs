using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Easytrade.Model.Migrations
{
    public partial class CorrectSeedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Allocation", "TotalProfit" },
                values: new object[] { 100000m, 12000m });

            migrationBuilder.UpdateData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Allocation", "TotalProfit" },
                values: new object[] { 15000m, 3000m });

            migrationBuilder.UpdateData(
                table: "ProfitsLosses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CompletionDate",
                value: new DateTime(2022, 12, 2, 13, 1, 1, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "ProfitsLosses",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CompletionDate",
                value: new DateTime(2022, 12, 4, 13, 1, 1, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "ProfitsLosses",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CompletionDate",
                value: new DateTime(2022, 12, 6, 13, 1, 1, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "SellOrders",
                keyColumn: "Id",
                keyValue: 3L,
                column: "BotId",
                value: 2L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Allocation", "TotalProfit" },
                values: new object[] { 1000m, 10000m });

            migrationBuilder.UpdateData(
                table: "Bots",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Allocation", "TotalProfit" },
                values: new object[] { 2000m, 0m });

            migrationBuilder.UpdateData(
                table: "ProfitsLosses",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CompletionDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProfitsLosses",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CompletionDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProfitsLosses",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CompletionDate",
                value: null);

            migrationBuilder.UpdateData(
                table: "SellOrders",
                keyColumn: "Id",
                keyValue: 3L,
                column: "BotId",
                value: 3L);
        }
    }
}
