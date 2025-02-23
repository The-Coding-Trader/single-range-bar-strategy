namespace SingleRangeBar.Strategy.Marketry.Models;

public interface IBar
{
    double Close { get; }
    double High { get; }
    double Low { get; }
    double Open { get; }
}