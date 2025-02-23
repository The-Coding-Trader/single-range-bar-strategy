using SingleRangeBar.Strategy.Marketry.Models;
using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Models;

public class NoTakeProfit : ITakeProfit
{
    public bool IsEmpty => true;

    public SlTpHolder ToSlTpHolder()
    {
        throw new InvalidOperationException("This should not be called when no take profit is set");
    }
}