using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Settings.Sections;

public class Risk(int sortIndex) : SettingsSection(GroupName, sortIndex)
{
    private int contracts;
    private int rangeBarSize;

    public int Contracts => contracts;
    public int RangeBarSize => rangeBarSize;

    public override IList<SettingItem> SettingItems =>
        new List<SettingItem>
        {
            new SettingItemInteger(Labels.Contracts, 1),
            new SettingItemInteger(Labels.RangeBarSize, 40)
        };

    public override void Update(IList<SettingItem> settingItems)
    {
        if (!settingItems.TryGetValue(GroupName, out List<SettingItem> updatedSettings) || updatedSettings == null)
            return;

        if (updatedSettings.TryGetValue(Labels.Contracts, out int updatedContracts))
            contracts = updatedContracts;
        if (updatedSettings.TryGetValue(Labels.RangeBarSize, out int updatedRangeBarSize))
            rangeBarSize = updatedRangeBarSize;
    }

    private static class Labels
    {
        public const string Contracts = "Contracts";
        public const string RangeBarSize = "Range Bar Size (in ticks)";
    }

    private const string GroupName = "Risk";
}