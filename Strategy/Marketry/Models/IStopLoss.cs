using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Marketry.Models;

public interface IStopLoss
{
    bool IsEmpty { get; }
    int Ticks { get; }
    SlTpHolder ToSlTpHolder();
}