using SingleRangeBar.Strategy.Marketry.Logging;
using SingleRangeBar.Strategy.Marketry.Models;
using TradingPlatform.BusinessLayer;
using ILogger = SingleRangeBar.Strategy.Marketry.Logging.ILogger;
using Side = SingleRangeBar.Strategy.Marketry.Models.Side;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Models;

public class Position : IPosition
{
    private readonly string id;
    private readonly ILogger logger;

    public string Id => QuantowerPosition?.Id ?? string.Empty;
    public bool IsKnown => true;
    public TradingPlatform.BusinessLayer.Position? QuantowerPosition => Core.Instance.GetPositionById(id);
    public Side Side => QuantowerPosition == null ? Side.Unknown : Side.FromQuantowerSide(QuantowerPosition.Side);

    public Position(string id) : this(id, Logger.Instance)
    {
    }

    public Position(string id, ILogger logger)
    {
        this.id = id;
        this.logger = logger;
    }

    public void AdjustStopLoss(IStopLoss stopLoss)
    {
        if (QuantowerPosition == null)
        {
            logger.Log($"QT Position {id} not found", LogLevel.Error);
            return;
        }

        if (QuantowerPosition.StopLoss == null)
        {
            logger.Log($"QT Position {id} does not have a stop loss", LogLevel.Error);
            return;
        }

        logger.Log(
            $"Adjusting stop loss of position {QuantowerPosition.Id} - {QuantowerPosition.OpenPrice} to {stopLoss.Ticks}");

        var modifyOrderRequestParameters = new ModifyOrderRequestParameters(QuantowerPosition.StopLoss)
        {
            Price = stopLoss.ToSlTpHolder().Price
        };

        Core.Instance.ModifyOrder(modifyOrderRequestParameters);
    }

    public void Close()
    {
        if (QuantowerPosition == null)
        {
            logger.Log($"QT Position {id} not found", LogLevel.Error);
            return;
        }

        logger.Log($"Closing position {QuantowerPosition.Id} - {QuantowerPosition.UniqueId}");

        QuantowerPosition.Close();
    }
}