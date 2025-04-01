using System.Collections;
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
    private bool isUpgradeOnCooldown = false;

    private void Start()
    {
        userData = GameManager.Instance.userData;
        statData = upgradeStatDataList.Find(x => x.statType == statType);
        UpdateUI();

        upgradeButton.onClick.AddListener(OnClickUpgrade);
    }

    private void OnClickUpgrade()
    {
        Debug.Log("업그레이드 버튼이 눌림!");

        if (isUpgradeOnCooldown) return;

        isUpgradeOnCooldown = true;
        StartCoroutine(UpgradeCooldown());

        int currentLevel = GetUpgradeLevel();
        float cost = statData.GetCostByLevel(currentLevel);

        if (userData.gold >= cost)
        {
            userData.gold -= cost;
            IncreaseUpgradeLevel();
            ApplyStatByLevel();
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
        statNameText.text = GetStatNameKorean(statType);
        levelText.text = $"레벨:{level}";
        valueText.text = $"{statData.GetValueByLevel(level)}";
        costText.text = $"가격: {FormatGold(statData.GetCostByLevel(level))} G";
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

    private IEnumerator UpgradeCooldown() /// 업그레이드 딜레이 추가 매서드
    {
        yield return new WaitForSeconds(0.3f); // 딜레이 0.3초
        isUpgradeOnCooldown = false;
    }

    private void ApplyStatByLevel() ///업그레이드 값 적용 매서드
    {
        int level = GetUpgradeLevel();
        float value = statData.GetValueByLevel(level);

        switch (statType)
        {
            case StatType.Atk:
                userData.atk = value;
                break;
            case StatType.CritHit:
                userData.critHit = value;
                break;
            case StatType.CritDmg:
                userData.critDmg = value;
                break;
            case StatType.AtkSpeed:
                userData.atkSpeed = value;
                break;
            case StatType.GoldBonus:
                userData.goldBonus = value;
                break;
            case StatType.ClickDmg:
                userData.clickDmg = value;
                break;
        }
    }

    public void OnClickUpgrade10x() ///10배 업그레이드 매서드
    {
        if (isUpgradeOnCooldown) return;

        isUpgradeOnCooldown = true;
        StartCoroutine(UpgradeCooldown());

        int currentLevel = GetUpgradeLevel();
        int maxLevel = Mathf.Min(currentLevel + 10, statData.valuePerLevel.Count);
        float totalCost = 0f;

        // 총 비용 계산
        for (int i = currentLevel; i < maxLevel; i++)
            totalCost += statData.GetCostByLevel(i);

        if (userData.gold >= totalCost)
        {
            userData.gold -= totalCost;

            // 레벨 증가
            for (int i = currentLevel; i < maxLevel; i++)
                IncreaseUpgradeLevel();

            ApplyStatByLevel(); // 최신 스탯 반영
            UpdateUI();
        }
        else
        {
            StartCoroutine(ShowWarning());
        }
    }

    private string GetStatNameKorean(StatType statType) ///한국어 패치
    {
        switch (statType)
        {
            case StatType.Atk: return "공격력";
            case StatType.CritHit: return "치명타 확률";
            case StatType.CritDmg: return "치명타 피해";
            case StatType.AtkSpeed: return "공격 속도";
            case StatType.GoldBonus: return "골드 보너스";
            case StatType.ClickDmg: return "클릭 데미지";
            default: return statType.ToString();
        }
    }

    private string FormatGold(float gold)
    {
        if (gold >= 1_000_000_000)
            return $"{gold / 1_000_000_000f:0.00}조";
        if (gold >= 100_000_000)
            return $"{gold / 100_000_000f:0.0}억";
        if (gold >= 10_000)
            return $"{gold / 10_000f:0.0}만";
        return $"{gold:N0}";
    }

}
