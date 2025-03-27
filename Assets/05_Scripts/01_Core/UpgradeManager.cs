using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public UserData userData;
    public List<UpgradeStatData> upgradeTables;

    public TMP_Text statValueText;
    public TMP_Text costText;
    public StatType upgradeTarget;

    private UpgradeStatData currentData;

    private void Start()
    {
        currentData = upgradeTables.Find(t => t.statType == upgradeTarget);
        RefreshUI();
    }

    public void OnClickUpgrade()
    {
        int level = GetCurrentLevel();
        float cost = currentData.GetCostByLevel(level);

        if (userData.gold >= cost)
        {
            userData.gold -= cost;
            IncreaseUpgradeLevel();
            RefreshUI();
        }
        else
        {
            Debug.Log("°ñµå ºÎÁ·!");
            // CoroutineÀ¸·Î UI °æ°íµµ ¶ç¿öÁàµµ ÁÁÀ½
        }
    }

    private void RefreshUI()
    {
        int level = GetCurrentLevel();
        statValueText.text = $"Lv.{level} - {currentData.GetValueByLevel(level)}";
        costText.text = $"{currentData.GetCostByLevel(level)} G";
    }

    private int GetCurrentLevel()
    {
        return upgradeTarget switch
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

    private void IncreaseUpgradeLevel()
    {
        switch (upgradeTarget)
        {
            case StatType.Atk: userData.upgradeData.atkLevel++; break;
            case StatType.CritHit: userData.upgradeData.critHitLevel++; break;
            case StatType.CritDmg: userData.upgradeData.critDmgLevel++; break;
            case StatType.AtkSpeed: userData.upgradeData.atkSpeedLevel++; break;
            case StatType.GoldBonus: userData.upgradeData.goldBonusLevel++; break;
            case StatType.ClickDmg: userData.upgradeData.clickDmgLevel++; break;
        }
    }
}
