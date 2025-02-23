using SingleRangeBar.Strategy.Marketry.Models;
using SingleRangeBar.Strategy.Marketry.Quantower.Models;
using TradingPlatform.BusinessLayer;
using Account = TradingPlatform.BusinessLayer.Account;
using IOrder = SingleRangeBar.Strategy.Marketry.Models.IOrder;
using IPosition = SingleRangeBar.Strategy.Marketry.Models.IPosition;
using Order = SingleRangeBar.Strategy.Marketry.Quantower.Models.Order;
using Position = SingleRangeBar.Strategy.Marketry.Quantower.Models.Position;
using Side = SingleRangeBar.Strategy.Marketry.Models.Side;

namespace SingleRangeBar.Strategy.Marketry.Quantower;

public class Broker(Core core, Account account, Symbol symbol) : IBroker
{
    public IEnumerable<IOrder> Orders => FindOrders();
    public IEnumerable<IPosition> Positions => FindPositions();

    public void Flatten()
    {
        foreach (var position in Positions) position.Close();

        foreach (var order in Orders) order.Cancel();
    }

    public IOrder Limit(Side side, double price, int quantity, IStopLoss stopLoss, ITakeProfit takeProfit)
    {
        var orderParams = CreateOrderParams(side, quantity, stopLoss, takeProfit);
        orderParams.OrderTypeId = OrderType.Limit;
        orderParams.Price = price;

        var result = core.PlaceOrder(orderParams);

        return new Order(result.OrderId);
    }

    public IOrder Market(Side side, int quantity)
    {
        return Market(side, quantity, new NoStopLoss(), new NoTakeProfit());
    }

    public IOrder Market(Side side, int quantity, IStopLoss stopLoss, ITakeProfit takeProfit)
    {
        var orderParams = CreateOrderParams(side, quantity, stopLoss, takeProfit);
        orderParams.OrderTypeId = OrderType.Market;

        var result = core.PlaceOrder(orderParams);

        return new Order(result.OrderId);
    }

    private PlaceOrderRequestParameters CreateOrderParams(Side side, int quantity, IStopLoss stopLoss,
        ITakeProfit takeProfit)
    {
        var orderParams = new PlaceOrderRequestParameters
        {
            Account = account,
            Symbol = symbol,
            TimeInForce = TimeInForce.Day,
            Side = side.ToQuantowerSide,
            Quantity = quantity
        };

        if (!stopLoss.IsEmpty)
            orderParams.StopLoss = stopLoss.ToSlTpHolder();

        if (!takeProfit.IsEmpty)
            orderParams.TakeProfit = takeProfit.ToSlTpHolder();

        return orderParams;
    }

    private IEnumerable<IOrder> FindOrders()
    {
        return core.Orders.Where(OrderWithSameAccountAndSymbol()).Select(o => new Order(o.Id));
    }

    private IEnumerable<IPosition> FindPositions()
    {
        return core.Positions.Where(PositionWithSameAccountAndSymbol()).Select(p => new Position(p.Id));
    }

    private Func<TradingPlatform.BusinessLayer.Order, bool> OrderWithSameAccountAndSymbol()
    {
        return order => order.Account.Equals(account) && order.Symbol.Equals(symbol);
    }

    private Func<TradingPlatform.BusinessLayer.Position, bool> PositionWithSameAccountAndSymbol()
    {
        return position => position.Account.Equals(account) && position.Symbol.Equals(symbol);
    }
}