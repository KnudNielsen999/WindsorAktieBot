using System;
using System.Collections.Generic;
using System.Text;

namespace AktieBotLibrary.Models;

public sealed class SignalRecord
{
    public DateTime Timestamp { get; set; }
    public string Symbol { get; set; } = "";
    public decimal Price { get; set; }
    public decimal Rsi { get; set; }
    public decimal Sma20 { get; set; }
    public decimal Sma50 { get; set; }
    public decimal Sma200 { get; set; }
    public bool OwnsStock { get; set; }
    public bool BuySignal { get; set; }
    public bool SellSignal { get; set; }
    public bool SetupIsGood { get; set; }
    public bool BlockedByOwnsStock { get; set; }
    public bool HardStopLossSignal { get; set; }
    public bool TrailingStopSignal { get; set; }
    public decimal EntryPrice { get; set; }
    public decimal HighestCloseSinceBuy { get; set; }
    public decimal TrailingStopPrice { get; set; }
    public bool TrailingActive { get; set; }
}