using System.Collections.Generic;
using System.Linq;

public class StatCalculator
{
    private UserData userData;
    private Dictionary<StatType, UpgradeStatData> upgradeTable;

    public StatCalculator(UserData data, List<UpgradeStatData> tables)
    {
        userData = data;
        upgradeTable = tables.ToDictionary(t => t.statType, t => t);
    }

    public float GetFinalStat(StatType stat)
    {
        int level = GetUpgradeLevel(stat);
        return upgradeTable[stat].GetValueByLevel(level);
    }

    private int GetUpgradeLevel(StatType stat)
    {
        return stat switch
        {
            StatType.Atk => userData.upgradeData.atkLevel,
            StatType.CritHit => userData.upgradeData.critHitLevel,
            StatType.CritDmg => userData.upgradeData.critDmgLevel,
            StatType.AtkSpeed => userData.upgradeData.atkSpeedLevel,
            StatType.GoldBonus => userData.upgradeData.goldBonusLevel,
            StatType.ClickDmg => userData.upgradeData.clickDmgLevel,
            _ => 0
        };
    }
}
