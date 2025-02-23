using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Settings;

public interface IConfiguration : Marketry.Quantower.IConfiguration
{
    Account Account { get; }
    int Contracts { get; }
    int RangeBarSize { get; }
    Symbol Symbol { get; }
}