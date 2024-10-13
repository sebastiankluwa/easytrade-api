﻿// <auto-generated />
using System;
using Easytrade.Model.DbAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Easytrade.Model.Migrations
{
    [DbContext(typeof(EasyTradeDbContext))]
    [Migration("20230212003836_CorrectSeedDB")]
    partial class CorrectSeedDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Easytrade.Model.Domain.Bots.Bot", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Allocation")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("DayProfit")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxOpenPositions")
                        .HasColumnType("integer");

                    b.Property<decimal>("MinimumAllocation")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string[]>("Symbols")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<decimal?>("TotalProfit")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Bots");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Allocation = 100000m,
                            DayProfit = 0m,
                            IsActive = true,
                            MaxOpenPositions = 5,
                            MinimumAllocation = 10m,
                            Name = "MACD Bot",
                            Symbols = new[] { "BTCUSDT" },
                            TotalProfit = 12000m
                        },
                        new
                        {
                            Id = 2L,
                            Allocation = 15000m,
                            DayProfit = 0m,
                            IsActive = true,
                            MaxOpenPositions = 2,
                            MinimumAllocation = 5m,
                            Name = "RSI Bot",
                            Symbols = new[] { "ETHUSDT" },
                            TotalProfit = 3000m
                        },
                        new
                        {
                            Id = 3L,
                            Allocation = 75m,
                            DayProfit = 0m,
                            IsActive = false,
                            MaxOpenPositions = 3,
                            MinimumAllocation = 7.5m,
                            Name = "EMA Bot",
                            Symbols = new[] { "LTCUSDT" },
                            TotalProfit = 0m
                        });
                });

            modelBuilder.Entity("Easytrade.Model.Domain.Bots.BuyOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<long>("BotId")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("Fee")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Pair")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ProfitLossId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric");

                    b.Property<long>("ReferenceOrderId")
                        .HasColumnType("bigint");

                    b.Property<string>("Side")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BotId");

                    b.ToTable("BuyOrders");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Amount = 10m,
                            BotId = 1L,
                            Fee = 0.5m,
                            OrderDate = new DateTime(2022, 12, 1, 13, 1, 1, 0, DateTimeKind.Utc),
                            Pair = "BTCUSDT",
                            ProfitLossId = 1L,
                            Rate = 10000m,
                            ReferenceOrderId = 1L,
                            Side = "Buy",
                            Status = 1
                        },
                        new
                        {
                            Id = 2L,
                            Amount = 20m,
                            BotId = 1L,
                            Fee = 1m,
                            OrderDate = new DateTime(2022, 12, 3, 13, 1, 1, 0, DateTimeKind.Utc),
                            Pair = "BTCUSDT",
                            ProfitLossId = 2L,
                            Rate = 1000m,
                            ReferenceOrderId = 2L,
                            Side = "Buy",
                            Status = 1
                        },
                        new
                        {
                            Id = 3L,
                            Amount = 30m,
                            BotId = 2L,
                            Fee = 1.5m,
                            OrderDate = new DateTime(2022, 12, 5, 13, 1, 1, 0, DateTimeKind.Utc),
                            Pair = "ETHUSDT",
                            ProfitLossId = 3L,
                            Rate = 400m,
                            ReferenceOrderId = 3L,
                            Side = "Buy",
                            Status = 1
                        },
                        new
                        {
                            Id = 4L,
                            Amount = 40m,
                            BotId = 2L,
                            Fee = 2m,
                            OrderDate = new DateTime(2022, 12, 7, 13, 1, 1, 0, DateTimeKind.Utc),
                            Pair = "ETHUSDT",
                            Rate = 200m,
                            ReferenceOrderId = 4L,
                            Side = "Buy",
                            Status = 0
                        });
                });

            modelBuilder.Entity("Easytrade.Model.Domain.Bots.ProfitLoss", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BuyOrderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("Result")
                        .HasColumnType("numeric");

                    b.Property<long>("SellOrderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BuyOrderId")
                        .IsUnique();

                    b.HasIndex("SellOrderId")
                        .IsUnique();

                    b.ToTable("ProfitsLosses");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            BuyOrderId = 1L,
                            CompletionDate = new DateTime(2022, 12, 2, 13, 1, 1, 0, DateTimeKind.Utc),
                            Result = 10000m,
                            SellOrderId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            BuyOrderId = 2L,
                            CompletionDate = new DateTime(2022, 12, 4, 13, 1, 1, 0, DateTimeKind.Utc),
                            Result = 2000m,
                            SellOrderId = 2L
                        },
                        new
                        {
                            Id = 3L,
                            BuyOrderId = 3L,
                            CompletionDate = new DateTime(2022, 12, 6, 13, 1, 1, 0, DateTimeKind.Utc),
                            Result = 3000m,
                            SellOrderId = 3L
                        });
                });

            modelBuilder.Entity("Easytrade.Model.Domain.Bots.SellOrder", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<long>("BotId")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("Fee")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Pair")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("ProfitLossId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Rate")
                        .HasColumnType("numeric");

                    b.Property<long>("ReferenceOrderId")
                        .HasColumnType("bigint");

                    b.Property<string>("Side")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BotId");

                    b.ToTable("SellOrders");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Amount = 10m,
                            BotId = 1L,
                            Fee = 0.5m,
                            OrderDate = new DateTime(2022, 12, 2, 13, 1, 1, 0, DateTimeKind.Utc),
                            Pair = "BTCUSDT",
                            ProfitLossId = 1L,
                            Rate = 11000m,
                            ReferenceOrderId = 1L,
                            Side = "Sell",
                            Status = 1
                        },
                        new
                        {
                            Id = 2L,
                            Amount = 20m,
                            BotId = 1L,
                            Fee = 1m,
                            OrderDate = new DateTime(2022, 12, 4, 13, 1, 1, 0, DateTimeKind.Utc),
                            Pair = "BTCUSDT",
                            ProfitLossId = 2L,
                            Rate = 1100m,
                            ReferenceOrderId = 2L,
                            Side = "Sell",
                            Status = 1
                        },
                        new
                        {
                            Id = 3L,
                            Amount = 30m,
                            BotId = 2L,
                            Fee = 2m,
                            OrderDate = new DateTime(2022, 12, 6, 13, 1, 1, 0, DateTimeKind.Utc),
                            Pair = "ETHUSDT",
                            ProfitLossId = 2L,
                            Rate = 500m,
                            ReferenceOrderId = 2L,
                            Side = "Sell",
                            Status = 1
                        });
                });

            modelBuilder.Entity("Easytrade.Model.Domain.Bots.BuyOrder", b =>
                {
                    b.HasOne("Easytrade.Model.Domain.Bots.Bot", "Bot")
                        .WithMany("BuyOrders")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bot");
                });

            modelBuilder.Entity("Easytrade.Model.Domain.Bots.ProfitLoss", b =>
                {
                    b.HasOne("Easytrade.Model.Domain.Bots.BuyOrder", "BuyOrder")
                        .WithOne("ProfitLoss")
                        .HasForeignKey("Easytrade.Model.Domain.Bots.ProfitLoss", "BuyOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Easytrade.Model.Domain.Bots.SellOrder", "SellOrder")
                        .WithOne("ProfitLoss")
                        .HasForeignKey("Easytrade.Model.Domain.Bots.ProfitLoss", "SellOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuyOrder");

                    b.Navigation("SellOrder");
                });

            modelBuilder.Entity("Easytrade.Model.Domain.Bots.SellOrder", b =>
                {
                    b.HasOne("Easytrade.Model.Domain.Bots.Bot", "Bot")
                        .WithMany("SellOrders")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bot");
                });

            modelBuilder.Entity("Easytrade.Model.Domain.Bots.Bot", b =>
                {
                    b.Navigation("BuyOrders");

                    b.Navigation("SellOrders");
                });

            modelBuilder.Entity("Easytrade.Model.Domain.Bots.BuyOrder", b =>
                {
                    b.Navigation("ProfitLoss");
                });

            modelBuilder.Entity("Easytrade.Model.Domain.Bots.SellOrder", b =>
                {
                    b.Navigation("ProfitLoss");
                });
#pragma warning restore 612, 618
        }
    }
}
