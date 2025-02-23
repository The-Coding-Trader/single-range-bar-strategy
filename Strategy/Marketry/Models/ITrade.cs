namespace SingleRangeBar.Strategy.Marketry.Models;

public interface ITrade
{
    string Id { get; }
    Side Side { get; }
}