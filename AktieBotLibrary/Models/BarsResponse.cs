using System;
using System.Collections.Generic;
using System.Text;

namespace AktieBotLibrary.Models;

public sealed class BarsResponse
{
    public Dictionary<string, List<BarDto>> Bars { get; set; } = new();
}
