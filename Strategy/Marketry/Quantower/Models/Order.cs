using SingleRangeBar.Strategy.Marketry.Logging;
using SingleRangeBar.Strategy.Marketry.Models;
using TradingPlatform.BusinessLayer;
using ILogger = SingleRangeBar.Strategy.Marketry.Logging.ILogger;
using IOrder = SingleRangeBar.Strategy.Marketry.Models.IOrder;
using Side = SingleRangeBar.Strategy.Marketry.Models.Side;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Models;

public class Order : IOrder
{
    private readonly string id;
    private readonly ILogger logger;

    public bool AttachedToBracket => QuantowerOrder != null && !string.IsNullOrEmpty(QuantowerOrder.GroupId);
    public string Id => QuantowerOrder?.Id ?? string.Empty;
    public bool IsKnown => true;
    public string OrderTypeId => QuantowerOrder?.OrderTypeId ?? string.Empty;
    public TradingPlatform.BusinessLayer.Order? QuantowerOrder => Core.Instance.GetOrderById(id);
    public Side Side => QuantowerOrder == null ? Side.Unknown : Side.FromQuantowerSide(QuantowerOrder.Side);

    public Order(string id) : this(id, Logger.Instance)
    {
    }

    public Order(string id, ILogger logger)
    {
        this.id = id;
        this.logger = logger;
    }

    public void AdjustStopLoss(IStopLoss stopLoss)
    {
        if (QuantowerOrder == null)
        {
            logger.Log($"QT Order {id} not found", LogLevel.Error);
            return;
        }

        logger.Log($"Adjusting stop loss of order {QuantowerOrder.Id} - {QuantowerOrder.Price} to {stopLoss.Ticks}");

        var modifyOrderRequestParameters = new ModifyOrderRequestParameters(QuantowerOrder)
        {
            StopLoss = stopLoss.ToSlTpHolder()
        };

        Core.Instance.ModifyOrder(modifyOrderRequestParameters);
    }

    public void Cancel()
    {
        if (QuantowerOrder == null)
        {
            logger.Log($"QT Order {id} not found", LogLevel.Error);
            return;
        }

        logger.Log($"Cancelling order {QuantowerOrder.Id} - {QuantowerOrder.UniqueId}");

        QuantowerOrder.Cancel();
    }
}