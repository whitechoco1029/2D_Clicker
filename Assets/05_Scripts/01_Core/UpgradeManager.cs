using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("플레이어 데이터 참조")]
    public UserData userData;

    [Header("업그레이드 데이터 리스트")]
    public List<UpgradeStatData> upgradeStatDataList;

    [Header("UI 요소")]
    public TMP_Text statNameText;
    public TMP_Text levelText;
    public TMP_Text valueText;
    public TMP_Text costText;
    public Button upgradeButton;
    public StatType statType;

    [Header("경고 메시지")]
    public GameObject warningPopup;
    public float warningDuration = 1.5f;

    private UpgradeStatData statData;

    private void Start()
    {
        statData = upgradeStatDataList.Find(x => x.statType == statType);
        UpdateUI();

        upgradeButton.onClick.AddListener(OnClickUpgrade);
    }

    private void OnClickUpgrade()
    {
        int currentLevel = GetUpgradeLevel();
        float cost = statData.GetCostByLevel(currentLevel);

        if (userData.gold >= cost)
        {
            userData.gold -= cost;
            IncreaseUpgradeLevel();
            UpdateUI();
        }
        else
        {
            StartCoroutine(ShowWarning());
        }
    }

    private void UpdateUI()
    {
        int level = GetUpgradeLevel();
        statNameText.text = statType.ToString();
        levelText.text = $"Lv. {level}";
        valueText.text = $"{statData.GetValueByLevel(level)}";
        costText.text = $"Cost: {statData.GetCostByLevel(level)} G";
    }

    private IEnumerator<WaitForSeconds> ShowWarning()
    {
        warningPopup.SetActive(true);
        yield return new WaitForSeconds(warningDuration);
        warningPopup.SetActive(false);
    }

    private int GetUpgradeLevel()
    {
        return statType switch
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
        switch (statType)
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
