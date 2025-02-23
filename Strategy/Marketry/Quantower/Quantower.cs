using SingleRangeBar.Strategy.Marketry.Models;
using TradingPlatform.BusinessLayer;
using Position = SingleRangeBar.Strategy.Marketry.Quantower.Models.Position;
using Trade = SingleRangeBar.Strategy.Marketry.Quantower.Models.Trade;

namespace SingleRangeBar.Strategy.Marketry.Quantower;

public class Quantower : IPlatform
{
    private readonly Core core;

    public event Action<IPosition>? PositionAdded;
    public event Action<IPosition>? PositionRemoved;
    public event Action<ITrade>? TradeAdded;

    public Quantower(Core core)
    {
        this.core = core;
        this.core.PositionAdded += OnPositionAdded;
        this.core.PositionRemoved += OnPositionRemoved;
        this.core.TradeAdded += OnTradeAdded;
    }

    public void Dispose()
    {
        core.PositionAdded -= OnPositionAdded;
        core.PositionRemoved -= OnPositionRemoved;
        core.TradeAdded -= OnTradeAdded;
    }

    private void OnPositionAdded(TradingPlatform.BusinessLayer.Position position)
    {
        PositionAdded?.Invoke(new Position(position.Id));
    }

    private void OnPositionRemoved(TradingPlatform.BusinessLayer.Position position)
    {
        PositionRemoved?.Invoke(new Position(position.Id));
    }

    private void OnTradeAdded(TradingPlatform.BusinessLayer.Trade trade)
    {
        TradeAdded?.Invoke(new Trade(trade));
    }
}