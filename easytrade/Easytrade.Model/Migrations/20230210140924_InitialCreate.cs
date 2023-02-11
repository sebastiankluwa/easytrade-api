using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Easytrade.Model.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bots",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Allocation = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalProfit = table.Column<decimal>(type: "numeric", nullable: true),
                    DayProfit = table.Column<decimal>(type: "numeric", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Symbols = table.Column<List<string>>(type: "text[]", nullable: false),
                    MaxOpenPositions = table.Column<int>(type: "integer", nullable: false),
                    MinimumAllocation = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BotId = table.Column<long>(type: "bigint", nullable: false),
                    ProfitLossId = table.Column<long>(type: "bigint", nullable: true),
                    ReferenceOrderId = table.Column<long>(type: "bigint", nullable: false),
                    Side = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Fee = table.Column<decimal>(type: "numeric", nullable: false),
                    Pair = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyOrders_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BotId = table.Column<long>(type: "bigint", nullable: false),
                    ProfitLossId = table.Column<long>(type: "bigint", nullable: true),
                    ReferenceOrderId = table.Column<long>(type: "bigint", nullable: false),
                    Side = table.Column<string>(type: "text", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Fee = table.Column<decimal>(type: "numeric", nullable: false),
                    Pair = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellOrders_Bots_BotId",
                        column: x => x.BotId,
                        principalTable: "Bots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfitsLosses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BuyOrderId = table.Column<long>(type: "bigint", nullable: false),
                    SellOrderId = table.Column<long>(type: "bigint", nullable: false),
                    Result = table.Column<decimal>(type: "numeric", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfitsLosses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfitsLosses_BuyOrders_BuyOrderId",
                        column: x => x.BuyOrderId,
                        principalTable: "BuyOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfitsLosses_SellOrders_SellOrderId",
                        column: x => x.SellOrderId,
                        principalTable: "SellOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bots_Name",
                table: "Bots",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuyOrders_BotId",
                table: "BuyOrders",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfitsLosses_BuyOrderId",
                table: "ProfitsLosses",
                column: "BuyOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfitsLosses_SellOrderId",
                table: "ProfitsLosses",
                column: "SellOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellOrders_BotId",
                table: "SellOrders",
                column: "BotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfitsLosses");

            migrationBuilder.DropTable(
                name: "BuyOrders");

            migrationBuilder.DropTable(
                name: "SellOrders");

            migrationBuilder.DropTable(
                name: "Bots");
        }
    }
}
