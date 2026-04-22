using AktieBotLibrary.Database;
using AktieBotLibrary.Models;

namespace AktieBotLibrary.Services;

public sealed class SteuchRsiBot
{
    private const int RsiPeriod = 14;

    private readonly BotDatabase _database;

    public SteuchRsiBot(BotDatabase? database = null)
    {
        _database = database ?? new BotDatabase();
    }

    public SignalRecord Evaluate(
        string symbol,
        IReadOnlyList<BarDto> bars,
        bool ownsStock = false,
        decimal entryPrice = 0m,
        decimal highestCloseSinceBuy = 0m)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(symbol);
        ArgumentNullException.ThrowIfNull(bars);

        if (bars.Count < 200)
        {
            throw new ArgumentException("At least 200 bars are required to evaluate the strategy.", nameof(bars));
        }

        var lastBar = bars[^1];
        var price = lastBar.Close;
        var rsi = CalculateRsi(bars, RsiPeriod);
        var sma20 = CalculateSma(bars, 20);
        var sma50 = CalculateSma(bars, 50);
        var sma200 = CalculateSma(bars, 200);
        var peakClose = ownsStock ? Math.Max(highestCloseSinceBuy, price) : 0m;

        var setupIsGood = price >= sma20 && sma20 >= sma50 && sma50 >= sma200;
        var blockedByOwnsStock = ownsStock;
        var buySignal = !blockedByOwnsStock && setupIsGood && rsi <= 35m;

        var hardStopLossSignal = ownsStock && entryPrice > 0m && price <= entryPrice * 0.92m;
        var trailingActive = ownsStock && entryPrice > 0m && peakClose >= entryPrice * 1.03m;
        var trailingStopPrice = trailingActive ? decimal.Round(peakClose * 0.95m, 4) : 0m;
        var trailingStopSignal = trailingActive && price <= trailingStopPrice;
        var sellSignal = ownsStock && (hardStopLossSignal || trailingStopSignal || (rsi >= 70m && price < sma20));

        var signal = new SignalRecord
        {
            Timestamp = lastBar.Time,
            Symbol = symbol.ToUpperInvariant(),
            Price = price,
            Rsi = rsi,
            Sma20 = sma20,
            Sma50 = sma50,
            Sma200 = sma200,
            OwnsStock = ownsStock,
            BuySignal = buySignal,
            SellSignal = sellSignal,
            SetupIsGood = setupIsGood,
            BlockedByOwnsStock = blockedByOwnsStock,
            HardStopLossSignal = hardStopLossSignal,
            TrailingStopSignal = trailingStopSignal,
            EntryPrice = entryPrice,
            HighestCloseSinceBuy = peakClose,
            TrailingStopPrice = trailingStopPrice,
            TrailingActive = trailingActive
        };

        _database.AddSignal(signal);
        return signal;
    }

    public TradeRecord CreateTrade(
        SignalRecord signal,
        string side,
        decimal quantity,
        string? note = null)
    {
        ArgumentNullException.ThrowIfNull(signal);
        ArgumentException.ThrowIfNullOrWhiteSpace(side);

        if (quantity <= 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity must be positive.");
        }

        var trade = new TradeRecord
        {
            Timestamp = signal.Timestamp,
            Symbol = signal.Symbol,
            Side = side.ToUpperInvariant(),
            Price = signal.Price,
            Quantity = quantity,
            TotalValue = decimal.Round(signal.Price * quantity, 4),
            Rsi = signal.Rsi,
            Sma20 = signal.Sma20,
            Sma50 = signal.Sma50,
            Sma200 = signal.Sma200,
            Note = note
        };

        _database.AddTrade(trade);
        return trade;
    }

    private static decimal CalculateSma(IReadOnlyList<BarDto> bars, int period)
    {
        if (bars.Count < period)
        {
            throw new ArgumentException($"At least {period} bars are required.", nameof(bars));
        }

        return decimal.Round(bars.Skip(bars.Count - period).Average(static bar => bar.Close), 4);
    }

    private static decimal CalculateRsi(IReadOnlyList<BarDto> bars, int period)
    {
        if (bars.Count <= period)
        {
            throw new ArgumentException($"At least {period + 1} bars are required.", nameof(bars));
        }

        decimal gains = 0m;
        decimal losses = 0m;

        for (var i = bars.Count - period; i < bars.Count; i++)
        {
            var change = bars[i].Close - bars[i - 1].Close;

            if (change > 0m)
            {
                gains += change;
            }
            else
            {
                losses -= change;
            }
        }

        if (losses == 0m)
        {
            return 100m;
        }

        var averageGain = gains / period;
        var averageLoss = losses / period;
        var relativeStrength = averageGain / averageLoss;

        return decimal.Round(100m - (100m / (1m + relativeStrength)), 2);
    }
}
