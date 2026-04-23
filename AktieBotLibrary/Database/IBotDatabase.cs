using AktieBotLibrary.Models;

namespace AktieBotLibrary.Database;

public interface IBotDatabase
{
    IReadOnlyList<SignalRecord> Signals { get; }

    IReadOnlyList<TradeRecord> Trades { get; }

    IReadOnlyList<PendingSellOrderRecord> PendingSellOrders { get; }

    IReadOnlyList<PositionRecord> Positions { get; }

    void AddSignal(SignalRecord signal);

    void AddTrade(TradeRecord trade);

    void AddPendingSellOrder(PendingSellOrderRecord order);

    void SavePosition(PositionRecord position);
}
