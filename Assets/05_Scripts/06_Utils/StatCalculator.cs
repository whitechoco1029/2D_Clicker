using System.Collections.Generic;
using System.Linq;

public class StatCalculator
{
    private UserData data;
    private Dictionary<StatType, UpgradeStatData> statTables;

    public StatCalculator(UserData data, List<UpgradeStatData> tables)
    {
        this.data = data;
        statTables = new();
        foreach (var table in tables)
            statTables[table.statType] = table;
    }

    public float GetStat(StatType stat)
    {
        int level = GetLevel(stat);
        return statTables[stat].GetValueByLevel(level);
    }

    private int GetLevel(StatType stat)
    {
        return stat switch
        {
            StatType.Atk => data.upgradeData.atkLevel,
            StatType.CritHit => data.upgradeData.critHitLevel,
            StatType.CritDmg => data.upgradeData.critDmgLevel,
            StatType.AtkSpeed => data.upgradeData.atkSpeedLevel,
            StatType.GoldBonus => data.upgradeData.goldBonusLevel,
            StatType.ClickDmg => data.upgradeData.clickDmgLevel,
            _ => 0
        };
    }
}
