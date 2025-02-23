using SingleRangeBar.Strategy.Marketry.Models;

namespace SingleRangeBar.Strategy.Marketry;

public interface IBroker
{
    IEnumerable<IOrder> Orders { get; }
    IEnumerable<IPosition> Positions { get; }
    void Flatten();
    IOrder Limit(Side side, double price, int quantity, IStopLoss stopLoss, ITakeProfit takeProfit);
    IOrder Market(Side side, int quantity);
    IOrder Market(Side side, int quantity, IStopLoss stopLoss, ITakeProfit takeProfit);
}