using AktieBotLibrary.Database;
using AktieBotLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace WindsorAktieBot.Data;

public sealed class EfBotDatabase(ApplicationDbContext dbContext) : IBotDatabase
{
    public IReadOnlyList<SignalRecord> Signals =>
        dbContext.Signals
            .AsNoTracking()
            .OrderByDescending(signal => signal.Timestamp)
            .ToList();

    public IReadOnlyList<TradeRecord> Trades =>
        dbContext.Trades
            .AsNoTracking()
            .OrderByDescending(trade => trade.Timestamp)
            .ToList();

    public IReadOnlyList<PendingSellOrderRecord> PendingSellOrders =>
        dbContext.PendingSellOrders
            .AsNoTracking()
            .OrderByDescending(order => order.CreatedAt)
            .ToList();

    public IReadOnlyList<PositionRecord> Positions =>
        dbContext.Positions
            .AsNoTracking()
            .OrderBy(position => position.Symbol)
            .ToList();

    public void AddSignal(SignalRecord signal)
    {
        ArgumentNullException.ThrowIfNull(signal);
        dbContext.Signals.Add(signal);
        dbContext.SaveChanges();
    }

    public void AddTrade(TradeRecord trade)
    {
        ArgumentNullException.ThrowIfNull(trade);
        dbContext.Trades.Add(trade);
        dbContext.SaveChanges();
    }

    public void AddPendingSellOrder(PendingSellOrderRecord order)
    {
        ArgumentNullException.ThrowIfNull(order);
        dbContext.PendingSellOrders.Add(order);
        dbContext.SaveChanges();
    }

    public void SavePosition(PositionRecord position)
    {
        ArgumentNullException.ThrowIfNull(position);

        var existingPosition = position.Id > 0
            ? dbContext.Positions.SingleOrDefault(current => current.Id == position.Id)
            : dbContext.Positions
                .OrderByDescending(current => current.Id)
                .FirstOrDefault(current => current.Symbol == position.Symbol && current.IsOpen);

        if (existingPosition is null)
        {
            dbContext.Positions.Add(position);
        }
        else
        {
            position.Id = existingPosition.Id;
            dbContext.Entry(existingPosition).CurrentValues.SetValues(position);
        }

        dbContext.SaveChanges();
    }
}
