using SingleRangeBar.Strategy.Settings.Sections;
using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Settings;

public class Configuration : IConfiguration
{
    private readonly General generalSettings;
    private readonly Risk riskSettings;
    private readonly int sortIndex = -1999;
    public Account Account => generalSettings.Account;
    public int Contracts => riskSettings.Contracts;
    public int RangeBarSize => riskSettings.RangeBarSize;

    public IEnumerable<SettingItem> Settings => BuildSettings();
    public Symbol Symbol => generalSettings.Symbol;

    public Configuration()
    {
        generalSettings = new General(++sortIndex);
        riskSettings = new Risk(++sortIndex);
    }

    public void Update(IList<SettingItem> settingItems)
    {
        generalSettings.Update(settingItems);
        riskSettings.Update(settingItems);
    }

    private IEnumerable<SettingItem> BuildSettings()
    {
        return new List<SettingItem>
        {
            generalSettings.Build(),
            riskSettings.Build()
        };
    }
}