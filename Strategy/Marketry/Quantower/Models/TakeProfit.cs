using SingleRangeBar.Strategy.Marketry.Models;
using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Models;

public class TakeProfit(int ticks) : ITakeProfit
{
    public bool IsEmpty => false;

    public SlTpHolder ToSlTpHolder()
    {
        return SlTpHolder.CreateTP(ticks, PriceMeasurement.Offset);
    }
}