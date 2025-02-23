using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Marketry.Quantower.Models;

public sealed class Asset : IQuantowerAsset
{
    private static readonly object PadLock = new();

    private static Asset instance;
    private readonly Symbol quantowerSymbol;
    private bool isInitialized;
    private double lastPrice;
    public event EventHandler<double>? PriceUpdated;

    public static IQuantowerAsset Instance
    {
        get
        {
            if (instance == null) throw new InvalidOperationException("Asset instance is not initialized");

            return instance;
        }
    }

    public bool IsKnown => true;

    private double LastPrice
    {
        set
        {
            if (Math.Abs(lastPrice - value) > double.Epsilon)
            {
                lastPrice = value;
                OnPriceUpdated(lastPrice);
            }
        }
    }

    public Symbol Symbol => quantowerSymbol;

    private Asset(Symbol quantowerSymbol)
    {
        this.quantowerSymbol = quantowerSymbol;
        lastPrice = quantowerSymbol.Last;

        quantowerSymbol.NewLast += NewLast;
        isInitialized = true;
    }

    public static void Initialize(Symbol quantowerSymbol)
    {
        if (quantowerSymbol == null) throw new ArgumentNullException(nameof(quantowerSymbol));

        if (instance != null) throw new InvalidOperationException("Asset instance is already initialized");

        lock (PadLock)
        {
            if (instance == null) instance = new Asset(quantowerSymbol);
        }
    }

    public static void Reset()
    {
        if (instance != null)
            lock (PadLock)
            {
                instance.Unsubscribe();
                instance = null;
            }
    }

    private void NewLast(Symbol symbol, Last last)
    {
        LastPrice = last.Price;
    }

    private void OnPriceUpdated(double price)
    {
        PriceUpdated?.Invoke(this, price);
    }

    private void Unsubscribe()
    {
        if (isInitialized)
        {
            quantowerSymbol.NewLast -= NewLast;
            isInitialized = false;
        }
    }
}