using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("�÷��̾� ������ ����")]
    public UserData userData;

    [Header("���׷��̵� ������ ����Ʈ")]
    public List<UpgradeStatData> upgradeStatDataList;

    [Header("UI ���")]
    public Text statNameText;
    public Text levelText;
    public Text valueText;
    public Text costText;
    public Button upgradeButton;
    public StatType statType;

    [Header("��� �޽���")]
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
        Debug.Log("���׷��̵� ��ư�� ����!");

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
        levelText.text = $"����:{level}";
        valueText.text = $"{statData.GetValueByLevel(level)}";
        costText.text = $"����: {FormatGold(statData.GetCostByLevel(level))} G";
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

    private IEnumerator UpgradeCooldown() /// ���׷��̵� ������ �߰� �ż���
    {
        yield return new WaitForSeconds(0.3f); // ������ 0.3��
        isUpgradeOnCooldown = false;
    }

    private void ApplyStatByLevel() ///���׷��̵� �� ���� �ż���
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

    public void OnClickUpgrade10x() ///10�� ���׷��̵� �ż���
    {
        if (isUpgradeOnCooldown) return;

        isUpgradeOnCooldown = true;
        StartCoroutine(UpgradeCooldown());

        int currentLevel = GetUpgradeLevel();
        int maxLevel = Mathf.Min(currentLevel + 10, statData.valuePerLevel.Count);
        float totalCost = 0f;

        // �� ��� ���
        for (int i = currentLevel; i < maxLevel; i++)
            totalCost += statData.GetCostByLevel(i);

        if (userData.gold >= totalCost)
        {
            userData.gold -= totalCost;

            // ���� ����
            for (int i = currentLevel; i < maxLevel; i++)
                IncreaseUpgradeLevel();

            ApplyStatByLevel(); // �ֽ� ���� �ݿ�
            UpdateUI();
        }
        else
        {
            StartCoroutine(ShowWarning());
        }
    }

    private string GetStatNameKorean(StatType statType) ///�ѱ��� ��ġ
    {
        switch (statType)
        {
            case StatType.Atk: return "���ݷ�";
            case StatType.CritHit: return "ġ��Ÿ Ȯ��";
            case StatType.CritDmg: return "ġ��Ÿ ����";
            case StatType.AtkSpeed: return "���� �ӵ�";
            case StatType.GoldBonus: return "��� ���ʽ�";
            case StatType.ClickDmg: return "Ŭ�� ������";
            default: return statType.ToString();
        }
    }

    private string FormatGold(float gold)
    {
        if (gold >= 1_000_000_000)
            return $"{gold / 1_000_000_000f:0.00}��";
        if (gold >= 100_000_000)
            return $"{gold / 100_000_000f:0.0}��";
        if (gold >= 10_000)
            return $"{gold / 10_000f:0.0}��";
        return $"{gold:N0}";
    }

}
