using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeStatData", menuName = "GameData/UpgradeStatData", order = 1)]
public class UpgradeStatData : ScriptableObject
{
    public StatType statType;
    public List<float> valuePerLevel;     // 레벨별 증가 수치
    public List<float> costPerLevel;      // 레벨별 업그레이드

    public float GetValueByLevel(int level)
    {
        if (level < valuePerLevel.Count)
            return valuePerLevel[level];
        return valuePerLevel[^1]; // 마지막 값
    }

    public float GetCostByLevel(int level)
    {
        if (level < costPerLevel.Count)
            return costPerLevel[level];
        return costPerLevel[^1];
    }
}

public enum StatType
{
    Atk,
    CritHit,
    CritDmg,
    AtkSpeed,
    GoldBonus,
    ClickDmg
}