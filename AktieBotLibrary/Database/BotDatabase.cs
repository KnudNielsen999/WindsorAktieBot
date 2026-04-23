using AktieBotLibrary.Models;

namespace AktieBotLibrary.Database;

public sealed class BotDatabase : IBotDatabase
{
    private readonly List<SignalRecord> _signals = [];
    private readonly List<TradeRecord> _trades = [];
    private readonly List<PendingSellOrderRecord> _pendingSellOrders = [];
    private readonly List<PositionRecord> _positions = [];

    public IReadOnlyList<SignalRecord> Signals => _signals;

    public IReadOnlyList<TradeRecord> Trades => _trades;

    public IReadOnlyList<PendingSellOrderRecord> PendingSellOrders => _pendingSellOrders;

    public IReadOnlyList<PositionRecord> Positions => _positions;

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

    public void SavePosition(PositionRecord position)
    {
        ArgumentNullException.ThrowIfNull(position);

        var existingIndex = _positions.FindIndex(current =>
            string.Equals(current.Symbol, position.Symbol, StringComparison.OrdinalIgnoreCase));

        if (existingIndex >= 0)
        {
            _positions[existingIndex] = position;
            return;
        }

        _positions.Add(position);
    }
}
