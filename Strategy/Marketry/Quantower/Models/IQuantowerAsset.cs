using SingleRangeBar.Strategy.Marketry.Models;
using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Models;

public interface IQuantowerAsset : IAsset
{
    Symbol Symbol { get; }
}