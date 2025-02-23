namespace SingleRangeBar.Strategy.Marketry.Models;

public interface IOrder
{
    bool AttachedToBracket { get; }
    string Id { get; }
    bool IsKnown { get; }
    string OrderTypeId { get; }
    Side Side { get; }
    void AdjustStopLoss(IStopLoss stopLoss);
    void Cancel();
}