using SingleRangeBar.Strategy.Marketry.Models;
using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Models;

public class StopLoss(int ticks) : IStopLoss
{
    public bool IsEmpty => false;
    public int Ticks => ticks;

    public SlTpHolder ToSlTpHolder()
    {
        return SlTpHolder.CreateSL(ticks, PriceMeasurement.Offset);
    }
}