namespace SingleRangeBar.Strategy.Marketry.Models;

public class Side : ValueObject<Side>
{
    public static readonly Side Buy = new(1, "Buy");
    public static readonly Side Sell = new(2, "Sell");
    public static readonly Side Unknown = new(3, "Unknown");

    public TradingPlatform.BusinessLayer.Side ToQuantowerSide
    {
        get
        {
            if (this == Buy)
                return TradingPlatform.BusinessLayer.Side.Buy;

            if (this == Sell)
                return TradingPlatform.BusinessLayer.Side.Sell;

            throw new ArgumentOutOfRangeException("Cannot return QT side for Unknown side");
        }
    }

    private Side(int id, string name) : base(id, name)
    {
    }

    public static Side FromQuantowerSide(TradingPlatform.BusinessLayer.Side quantowerOrderSide)
    {
        return quantowerOrderSide switch
        {
            TradingPlatform.BusinessLayer.Side.Buy => Buy,
            TradingPlatform.BusinessLayer.Side.Sell => Sell,
            _ => Unknown
        };
    }
}