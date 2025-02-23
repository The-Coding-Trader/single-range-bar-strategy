using TradingPlatform.BusinessLayer;

namespace SingleRangeBar.Strategy.Settings.Sections;

public class General(int sortIndex) : SettingsSection(GroupName, sortIndex)
{
    private Account account;
    private Symbol symbol;
    public Account Account => account;

    public override IList<SettingItem> SettingItems =>
        new List<SettingItem>
        {
            new SettingItemSymbol(Labels.Symbol, symbol),
            new SettingItemAccount(Labels.Account, account)
        };

    public Symbol Symbol => symbol;

    public override void Update(IList<SettingItem> settingItems)
    {
        if (!settingItems.TryGetValue(GroupName, out List<SettingItem> updatedSettings) || updatedSettings == null)
            return;

        if (updatedSettings.TryGetValue(Labels.Symbol, out Symbol updatedSymbol))
            symbol = updatedSymbol;
        if (updatedSettings.TryGetValue(Labels.Account, out Account updatedAccount))
            account = updatedAccount;
    }

    private static class Labels
    {
        public const string Symbol = "Symbol";
        public const string Account = "Account";
    }

    private const string GroupName = "General";
}