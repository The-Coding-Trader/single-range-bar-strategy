using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Marketry.Quantower;

public interface IConfiguration
{
    IEnumerable<SettingItem> Settings { get; }
    void Update(IList<SettingItem> settingItems);
}