using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Settings;

public abstract class SettingsSection(string separatorGroupName, int sortIndex)
    : ISettingsSection
{
    private readonly SettingItemSeparatorGroup separatorGroup = new(separatorGroupName, sortIndex);
    public string SeparatorGroupName => separatorGroup.Text;

    public abstract IList<SettingItem> SettingItems { get; }

    public virtual SettingItem Build()
    {
        var updatedSettingItems = SettingItems
            .Select(settingItem =>
            {
                settingItem.SeparatorGroup = separatorGroup;
                return settingItem;
            })
            .ToList();

        return new SettingItemGroup(separatorGroupName, updatedSettingItems);
    }

    public abstract void Update(IList<SettingItem> settingItems);
}