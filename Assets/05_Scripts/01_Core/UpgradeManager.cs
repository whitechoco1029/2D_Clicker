using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("플레이어 데이터 참조")]
    public UserData userData;

    [Header("업그레이드 데이터 리스트")]
    public List<UpgradeStatData> upgradeStatDataList;

    [Header("UI 요소")]
    public Text statNameText;
    public Text levelText;
    public Text valueText;
    public Text costText;
    public Button upgradeButton;
    public StatType statType;

    [Header("경고 메시지")]
    public GameObject warningPopup;
    public float warningDuration = 1.5f;

    private UpgradeStatData statData;

    private void Start()
    {
        userData = GameManager.Instance.userData;
        statData = upgradeStatDataList.Find(x => x.statType == statType);
        UpdateUI();

        upgradeButton.onClick.AddListener(OnClickUpgrade);
    }

    public void OnClickUpgrade()
    {
        Debug.Log("업그레이드 버튼 눌림!"); 

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
        levelText.text = $"레벨:{level}";
        valueText.text = $"{statData.GetValueByLevel(level)}";
        costText.text = $"가격: {statData.GetCostByLevel(level)} G";
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
