using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;

namespace AktieBotLibrary.Models;

public sealed class BarDto
{
    public DateTime Time { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }

    public string t
    {
        set => Time = DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
    }

    public decimal o { set => Open = value; }
    public decimal h { set => High = value; }
    public decimal l { set => Low = value; }
    public decimal c { set => Close = value; }
    public decimal v { set => Volume = value; }
}
