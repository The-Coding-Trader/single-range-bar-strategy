using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Settings;

public interface ISettingsSection
{
    string SeparatorGroupName { get; }
    SettingItem Build();
    void Update(IList<SettingItem> settingItems);
}