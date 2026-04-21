using System;
using System.Collections.Generic;
using System.Text;
namespace AktieBotLibrary.Models;

public sealed class PendingSellOrderRecord
{
    public string Symbol { get; set; } = "";
    public Guid OrderId { get; set; }
    public decimal Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = "";
    public bool IsProcessed { get; set; }
}