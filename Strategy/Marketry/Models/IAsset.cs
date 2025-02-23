namespace SingleRangeBar.Strategy.Marketry.Models;

public interface IAsset
{
    event EventHandler<double> PriceUpdated;
    bool IsKnown { get; }
}