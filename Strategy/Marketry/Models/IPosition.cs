namespace SingleRangeBar.Strategy.Marketry.Models;

public interface IPosition
{
    string Id { get; }
    Side Side { get; }
    void AdjustStopLoss(IStopLoss stopLoss);
    void Close();
}