using System.Reflection;
using SingleRangeBar.Strategy.Marketry.Logging;
using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Logging;

public class QuantowerLogger : IQuantowerLogger
{
    private readonly MethodInfo? logMethod;
    private readonly Strategy strategy;

    public QuantowerLogger(Strategy strategy)
    {
        this.strategy = strategy;
        logMethod = strategy.GetType().GetMethod("Log", BindingFlags.Instance | BindingFlags.NonPublic);

        if (logMethod == null)
            throw new InvalidOperationException("Could not find Log method in strategy");
    }

    public void Log(string message, LogLevel level = LogLevel.Info)
    {
        var quantowerLevel = level switch
        {
            LogLevel.Info => StrategyLoggingLevel.Info,
            LogLevel.Error => StrategyLoggingLevel.Error,
            _ => StrategyLoggingLevel.Info
        };

        logMethod?.Invoke(strategy, [message, quantowerLevel]);
    }

    public void Log(Order order, Verbosity verbosity = Verbosity.Info)
    {
        switch (verbosity)
        {
            case Verbosity.Info:
                Log($"Order: {order.Id} - {order.UniqueId}");
                Log(
                    $"==== {order.OrderType.Name} - {order.Side} - {order.Status} - Price: {order.Price} - Trigger Price: {order.TriggerPrice}");
                break;
            case Verbosity.Debug:
                Log($"Order: {order.Id} - {order.UniqueId}");
                Log($"==== Account: {order.Account.Id} - {order.Account.Name}");
                Log($"==== Average Fill Price: {order.AverageFillPrice}");
                Log($"==== Comment: {order.Comment}");
                Log($"==== Connection: {order.ConnectionId} - {order.Connection.Name}");
                Log($"==== Filled Quantity: {order.FilledQuantity}");
                Log($"==== Group ID: {order.GroupId}");
                Log($"==== Last Update Time: {order.LastUpdateTime}");
                Log($"==== Order Type: {order.OrderTypeId} - {order.OrderType.Name}");
                Log($"==== Original Status: {order.OriginalStatus}");
                Log($"==== Position: {order.PositionId}");
                Log($"==== Price: {order.Price}");
                Log($"==== Remaining Quantity: {order.RemainingQuantity}");
                Log($"==== Side: {order.Side}");
                Log($"==== State: {order.State}");
                Log($"==== Status: {order.Status}");
                Log($"==== Stop Loss: {order.StopLoss?.Price} - {order.StopLoss?.Quantity}");
                Log($"==== Symbol: {order.Symbol.Name}");
                Log($"==== Take Profit: {order.TakeProfit?.Price} - {order.TakeProfit?.Quantity}");
                Log($"==== Total Quantity: {order.TotalQuantity}");
                Log($"==== Trail Offset: {order.TrailOffset}");
                Log($"==== Trigger Price: {order.TriggerPrice}");
                Log($"==== Unique ID: {order.UniqueId}");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(verbosity), verbosity, null);
        }
    }

    public void Log(Position position, Verbosity verbosity = Verbosity.Info)
    {
        switch (verbosity)
        {
            case Verbosity.Info:
                Log($"Position: {position.Id} - {position.UniqueId} - {position.Side}");
                Log(
                    $"==== {position.Side} - {position.State} - Open Price: {position.OpenPrice} - Current Price: {position.CurrentPrice}");
                break;
            case Verbosity.Debug:
                Log($"Position: {position.Id} - {position.UniqueId}");
                Log($"==== Account: {position.Account.Id} - {position.Account.Name}");
                Log($"==== Comment: {position.Comment}");
                Log($"==== Connection: {position.ConnectionId} - {position.Connection.Name}");
                Log($"==== Current Price: {position.CurrentPrice}");
                Log($"==== Fee: {position.Fee}");
                Log($"==== Gross PnL: {position.GrossPnL.Value}");
                Log($"==== Gross PnL Ticks: {position.GrossPnLTicks}");
                Log($"==== Liquidation Price: {position.LiquidationPrice}");
                Log($"==== Net PnL: {position.NetPnL.Value}");
                Log($"==== Open Price: {position.OpenPrice}");
                Log($"==== Open Time: {position.OpenTime}");
                Log($"==== Quantity: {position.Quantity}");
                Log($"==== Side: {position.Side}");
                Log($"==== State: {position.State}");
                Log($"==== Stop Loss: {position.StopLoss?.Side} - {position.StopLoss?.Price}");
                Log($"==== Symbol: {position.Symbol.Id} {position.Symbol.Name}");
                Log($"==== Take Profit: {position.TakeProfit?.Side} - {position.TakeProfit?.Price}");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(verbosity), verbosity, null);
        }
    }

    public void Separator()
    {
        Log("==========");
    }
}