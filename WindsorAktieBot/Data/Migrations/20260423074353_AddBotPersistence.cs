using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WindsorAktieBot.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBotPersistence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotPendingSellOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    StopPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    PositionRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotPendingSellOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BotPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    EntryPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    HighestCloseSinceBuy = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    StopLossPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    TrailingStopPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    TrailingActive = table.Column<bool>(type: "bit", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    OpenedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastMarketPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BotSignals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Rsi = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Sma20 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Sma50 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Sma200 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    OwnsStock = table.Column<bool>(type: "bit", nullable: false),
                    BuySignal = table.Column<bool>(type: "bit", nullable: false),
                    SellSignal = table.Column<bool>(type: "bit", nullable: false),
                    SetupIsGood = table.Column<bool>(type: "bit", nullable: false),
                    BlockedByOwnsStock = table.Column<bool>(type: "bit", nullable: false),
                    HardStopLossSignal = table.Column<bool>(type: "bit", nullable: false),
                    TrailingStopSignal = table.Column<bool>(type: "bit", nullable: false),
                    EntryPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    HighestCloseSinceBuy = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TrailingStopPrice = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    TrailingActive = table.Column<bool>(type: "bit", nullable: false),
                    PositionRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotSignals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BotTrades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Side = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Rsi = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Sma20 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Sma50 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Sma200 = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealizedPnLUsd = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    RealizedPnLDkk = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsMatched = table.Column<bool>(type: "bit", nullable: false),
                    BrokerOrderId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    SignalRecordId = table.Column<int>(type: "int", nullable: true),
                    PositionRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotTrades", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotPendingSellOrders_OrderId",
                table: "BotPendingSellOrders",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BotPendingSellOrders_Symbol_IsProcessed",
                table: "BotPendingSellOrders",
                columns: new[] { "Symbol", "IsProcessed" });

            migrationBuilder.CreateIndex(
                name: "IX_BotPositions_Symbol_IsOpen",
                table: "BotPositions",
                columns: new[] { "Symbol", "IsOpen" });

            migrationBuilder.CreateIndex(
                name: "IX_BotSignals_Symbol_Timestamp",
                table: "BotSignals",
                columns: new[] { "Symbol", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_BotTrades_Symbol_Timestamp",
                table: "BotTrades",
                columns: new[] { "Symbol", "Timestamp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotPendingSellOrders");

            migrationBuilder.DropTable(
                name: "BotPositions");

            migrationBuilder.DropTable(
                name: "BotSignals");

            migrationBuilder.DropTable(
                name: "BotTrades");
        }
    }
}
