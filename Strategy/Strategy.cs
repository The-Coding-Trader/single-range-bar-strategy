using SingleRangeBar.Strategy.Marketry;
using SingleRangeBar.Strategy.Marketry.Logging;
using SingleRangeBar.Strategy.Marketry.Quantower;
using SingleRangeBar.Strategy.Marketry.Quantower.Logging;
using SingleRangeBar.Strategy.Settings;
using TradingPlatform.BusinessLayer;
using Asset = SingleRangeBar.Strategy.Marketry.Quantower.Models.Asset;
using IConfiguration = SingleRangeBar.Strategy.Settings.IConfiguration;

namespace SingleRangeBar.Strategy;

public class Strategy : TradingPlatform.BusinessLayer.Strategy
{
    private readonly IConfiguration configuration;
    private IAlgo algo;

    public override IList<SettingItem> Settings
    {
        get
        {
            var settings = base.Settings;

            settings.AddRange(configuration.Settings);

            return settings;
        }
        set
        {
            base.Settings = value;

            configuration.Update(value);
        }
    }

    public Strategy()
    {
        Name = "Single Range Bar";
        Description = "Simple.  When it goes up, Buy.  When it goes down, Sell.";

        configuration = new Configuration();
    }

    protected override void OnCreated()
    {
        Logger.Initialize(new QuantowerLogger(this));
        Logger.Instance.Log("Created strategy");
    }

    protected override void OnRemove()
    {
        Logger.Reset();
    }

    protected override void OnRun()
    {
        Logger.Instance.Log("Running strategy");

        Asset.Initialize(configuration.Symbol);

        var platform = new Quantower(Core.Instance);
        var broker = new Broker(Core.Instance, configuration.Account, configuration.Symbol);

        algo = new Algo(platform, broker, Asset.Instance, Logger.Instance, configuration);
        algo.Start();
    }

    protected override void OnStop()
    {
        algo.Stop();
        Asset.Reset();
    }
}