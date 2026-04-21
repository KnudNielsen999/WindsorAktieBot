using System;
using System.Collections.Generic;
using System.Text;

namespace AktieBotLibrary.Models;

public sealed class TradeRecord
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string Symbol { get; set; } = "";
    public string Side { get; set; } = "";
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public decimal TotalValue { get; set; }
    public decimal Rsi { get; set; }
    public decimal Sma20 { get; set; }
    public decimal Sma50 { get; set; }
    public decimal Sma200 { get; set; }
    public string? Note { get; set; }
    public decimal RealizedPnLUsd { get; set; }
    public decimal RealizedPnLDkk { get; set; }
    public bool IsMatched { get; set; }
}