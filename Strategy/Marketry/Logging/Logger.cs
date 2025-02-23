namespace SingleRangeBar.Strategy.Marketry.Logging;

public class Logger : ILogger
{
    private static readonly Lazy<Logger> instance = new(() => new Logger());
    private ILogger underlyingLogger;

    public static Logger Instance => instance.Value;

    private Logger()
    {
    }

    public static void Initialize(ILogger underlyingLogger)
    {
        if (Instance.underlyingLogger == null)
            Instance.underlyingLogger = underlyingLogger ?? throw new ArgumentNullException(nameof(underlyingLogger));
        else
            throw new InvalidOperationException("Logger has already been initialized.");
    }

    public void Log(string message, LogLevel level = LogLevel.Info)
    {
        if (underlyingLogger == null) throw new InvalidOperationException("Logger has not been initialized.");

        underlyingLogger.Log(message, level);
    }

    public static void Reset()
    {
        Instance.underlyingLogger = null;
    }

    public void Separator()
    {
        if (underlyingLogger == null) throw new InvalidOperationException("Logger has not been initialized.");

        underlyingLogger.Separator();
    }
}