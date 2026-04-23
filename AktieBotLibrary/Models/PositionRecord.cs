namespace AktieBotLibrary.Models;

public sealed class PositionRecord
{
    public int Id { get; set; }
    public string Symbol { get; set; } = "";
    public decimal Quantity { get; set; }
    public decimal EntryPrice { get; set; }
    public decimal HighestCloseSinceBuy { get; set; }
    public decimal? StopLossPrice { get; set; }
    public decimal? TrailingStopPrice { get; set; }
    public bool TrailingActive { get; set; }
    public bool IsOpen { get; set; }
    public DateTime OpenedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public decimal? LastMarketPrice { get; set; }
    public string? Note { get; set; }
}
