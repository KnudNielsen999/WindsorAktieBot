using AktieBotLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WindsorAktieBot.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<SignalRecord> Signals => Set<SignalRecord>();

        public DbSet<TradeRecord> Trades => Set<TradeRecord>();

        public DbSet<PendingSellOrderRecord> PendingSellOrders => Set<PendingSellOrderRecord>();

        public DbSet<PositionRecord> Positions => Set<PositionRecord>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SignalRecord>(entity =>
            {
                entity.ToTable("BotSignals");
                entity.HasKey(signal => signal.Id);
                entity.Property(signal => signal.Symbol).HasMaxLength(16);
                entity.Property(signal => signal.Price).HasColumnType("decimal(18,4)");
                entity.Property(signal => signal.Rsi).HasColumnType("decimal(18,4)");
                entity.Property(signal => signal.Sma20).HasColumnType("decimal(18,4)");
                entity.Property(signal => signal.Sma50).HasColumnType("decimal(18,4)");
                entity.Property(signal => signal.Sma200).HasColumnType("decimal(18,4)");
                entity.Property(signal => signal.EntryPrice).HasColumnType("decimal(18,4)");
                entity.Property(signal => signal.HighestCloseSinceBuy).HasColumnType("decimal(18,4)");
                entity.Property(signal => signal.TrailingStopPrice).HasColumnType("decimal(18,4)");
                entity.HasIndex(signal => new { signal.Symbol, signal.Timestamp });
            });

            builder.Entity<TradeRecord>(entity =>
            {
                entity.ToTable("BotTrades");
                entity.HasKey(trade => trade.Id);
                entity.Property(trade => trade.Symbol).HasMaxLength(16);
                entity.Property(trade => trade.Side).HasMaxLength(8);
                entity.Property(trade => trade.BrokerOrderId).HasMaxLength(128);
                entity.Property(trade => trade.Price).HasColumnType("decimal(18,4)");
                entity.Property(trade => trade.Quantity).HasColumnType("decimal(18,6)");
                entity.Property(trade => trade.TotalValue).HasColumnType("decimal(18,4)");
                entity.Property(trade => trade.Rsi).HasColumnType("decimal(18,4)");
                entity.Property(trade => trade.Sma20).HasColumnType("decimal(18,4)");
                entity.Property(trade => trade.Sma50).HasColumnType("decimal(18,4)");
                entity.Property(trade => trade.Sma200).HasColumnType("decimal(18,4)");
                entity.Property(trade => trade.RealizedPnLUsd).HasColumnType("decimal(18,4)");
                entity.Property(trade => trade.RealizedPnLDkk).HasColumnType("decimal(18,4)");
                entity.HasIndex(trade => new { trade.Symbol, trade.Timestamp });
            });

            builder.Entity<PendingSellOrderRecord>(entity =>
            {
                entity.ToTable("BotPendingSellOrders");
                entity.HasKey(order => order.Id);
                entity.Property(order => order.Symbol).HasMaxLength(16);
                entity.Property(order => order.Status).HasMaxLength(32);
                entity.Property(order => order.Quantity).HasColumnType("decimal(18,6)");
                entity.Property(order => order.StopPrice).HasColumnType("decimal(18,4)");
                entity.HasIndex(order => order.OrderId).IsUnique();
                entity.HasIndex(order => new { order.Symbol, order.IsProcessed });
            });

            builder.Entity<PositionRecord>(entity =>
            {
                entity.ToTable("BotPositions");
                entity.HasKey(position => position.Id);
                entity.Property(position => position.Symbol).HasMaxLength(16);
                entity.Property(position => position.Quantity).HasColumnType("decimal(18,6)");
                entity.Property(position => position.EntryPrice).HasColumnType("decimal(18,4)");
                entity.Property(position => position.HighestCloseSinceBuy).HasColumnType("decimal(18,4)");
                entity.Property(position => position.StopLossPrice).HasColumnType("decimal(18,4)");
                entity.Property(position => position.TrailingStopPrice).HasColumnType("decimal(18,4)");
                entity.Property(position => position.LastMarketPrice).HasColumnType("decimal(18,4)");
                entity.HasIndex(position => new { position.Symbol, position.IsOpen });
            });
        }
    }
}
