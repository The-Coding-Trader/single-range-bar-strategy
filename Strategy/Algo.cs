using SingleRangeBar.Strategy.Marketry;
using SingleRangeBar.Strategy.Marketry.Models;
using SingleRangeBar.Strategy.Marketry.Quantower.Models;
using TradingPlatform.BusinessLayer;
using ILogger = SingleRangeBar.Strategy.Marketry.Logging.ILogger;
using IConfiguration = SingleRangeBar.Strategy.Settings.IConfiguration;
using Side = SingleRangeBar.Strategy.Marketry.Models.Side;

namespace SingleRangeBar.Strategy;

public class Algo(
    IPlatform platform,
    IBroker broker,
    IQuantowerAsset asset,
    ILogger logger,
    IConfiguration configuration) : IAlgo
{
    private HistoricalData rangeBarData;
    private IHistoryItem PreviousRangeBar => rangeBarData[1];

    public void Start()
    {
        logger.Log("Starting Algo");

        asset.PriceUpdated += PriceUpdated;
        platform.PositionAdded += PositionAdded;
        platform.PositionRemoved += PositionRemoved;
        platform.TradeAdded += TradeAdded;

        rangeBarData = asset.Symbol.GetHistory(new HistoryRequestParameters
        {
            Symbol = asset.Symbol,
            FromTime = Core.Instance.TimeUtils.DateTimeUtcNow.AddDays(-10),
            ToTime = default,
            Aggregation = new HistoryAggregationRangeBars(configuration.RangeBarSize, asset.Symbol.HistoryType)
        });

        rangeBarData.NewHistoryItem += OnNewHistoryItem;
    }

    public void Stop()
    {
        logger.Log("Stopping Algo");

        broker.Flatten();

        asset.PriceUpdated -= PriceUpdated;
        platform.PositionAdded -= PositionAdded;
        platform.PositionRemoved -= PositionRemoved;
        platform.TradeAdded -= TradeAdded;
        rangeBarData.NewHistoryItem -= OnNewHistoryItem;

        platform.Dispose();
    }

    private void OnNewHistoryItem(object sender, HistoryEventArgs e)
    {
        broker.Flatten();
        broker.Market(
            PreviousRangeBar[PriceType.Open] < PreviousRangeBar[PriceType.Close] ? Side.Buy : Side.Sell,
            configuration.Contracts
        );
    }

    private void PositionAdded(IPosition position)
    {
        // do something when a position is added
    }

    private void PositionRemoved(IPosition position)
    {
        // do something when a position is removed
    }

    private void PriceUpdated(object? sender, double price)
    {
        // do something when a price is updated
    }

    private void TradeAdded(ITrade trade)
    {
        // do something when a trade is added
    }
}