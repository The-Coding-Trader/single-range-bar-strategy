using SingleRangeBar.Strategy.Marketry.Logging;
using SingleRangeBar.Strategy.Marketry.Models;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Models;

public class Trade : ITrade
{
    private readonly ILogger logger;
    private readonly TradingPlatform.BusinessLayer.Trade quantowerTrade;

    public string Id => quantowerTrade.Id;
    public Side Side => Side.FromQuantowerSide(quantowerTrade.Side);

    public Trade(TradingPlatform.BusinessLayer.Trade quantowerTrade) : this(quantowerTrade, Logger.Instance)
    {
    }

    public Trade(TradingPlatform.BusinessLayer.Trade quantowerTrade, ILogger logger)
    {
        this.quantowerTrade = quantowerTrade;
        this.logger = logger;
    }
}