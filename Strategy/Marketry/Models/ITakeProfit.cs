using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Marketry.Models;

public interface ITakeProfit
{
    bool IsEmpty { get; }
    SlTpHolder ToSlTpHolder();
}