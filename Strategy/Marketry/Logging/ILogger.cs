namespace SingleRangeBar.Strategy.Marketry.Logging;

public interface ILogger
{
    void Log(string message, LogLevel level = LogLevel.Info);
    void Separator();
}