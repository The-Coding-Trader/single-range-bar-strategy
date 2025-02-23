using SingleRangeBar.Strategy.Marketry.Models;
using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Models;

public class NoStopLoss : IStopLoss
{
    public bool IsEmpty => true;
    public int Ticks => 0;

    public SlTpHolder ToSlTpHolder()
    {
        throw new InvalidOperationException("This should not be called when no stop loss is set");
    }
}