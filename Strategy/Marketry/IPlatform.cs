using SingleRangeBar.Strategy.Marketry.Models;

namespace SingleRangeBar.Strategy.Marketry;

public interface IPlatform : IDisposable
{
    event Action<IPosition> PositionAdded;
    event Action<IPosition> PositionRemoved;
    event Action<ITrade> TradeAdded;
}