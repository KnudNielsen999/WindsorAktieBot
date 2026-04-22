using AktieBotLibrary.Models;

namespace AktieBotLibrary.Database;

public sealed class BotDatabase
{
    private readonly List<SignalRecord> _signals = [];
    private readonly List<TradeRecord> _trades = [];
    private readonly List<PendingSellOrderRecord> _pendingSellOrders = [];

    public IReadOnlyList<SignalRecord> Signals => _signals;

    public IReadOnlyList<TradeRecord> Trades => _trades;

    public IReadOnlyList<PendingSellOrderRecord> PendingSellOrders => _pendingSellOrders;

    public void AddSignal(SignalRecord signal)
    {
        ArgumentNullException.ThrowIfNull(signal);
        _signals.Add(signal);
    }

    public void AddTrade(TradeRecord trade)
    {
        ArgumentNullException.ThrowIfNull(trade);
        _trades.Add(trade);
    }

    public void AddPendingSellOrder(PendingSellOrderRecord order)
    {
        ArgumentNullException.ThrowIfNull(order);
        _pendingSellOrders.Add(order);
    }
}
