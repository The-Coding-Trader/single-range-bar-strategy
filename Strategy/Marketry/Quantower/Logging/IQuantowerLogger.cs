using TradingPlatform.BusinessLayer;
using ILogger = SingleRangeBar.Strategy.Marketry.Logging.ILogger;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Logging;

public interface IQuantowerLogger : ILogger
{
    void Log(Order order, Verbosity verbosity = Verbosity.Info);
    void Log(Position position, Verbosity verbosity = Verbosity.Info);
}