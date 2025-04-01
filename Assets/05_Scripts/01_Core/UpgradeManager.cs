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
        statNameText.text = statType.ToString();
        levelText.text = $"����:{level}";
        valueText.text = $"{statData.GetValueByLevel(level)}";
        costText.text = $"����: {statData.GetCostByLevel(level)} G";
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

    private IEnumerator UpgradeCooldown()
    {
        yield return new WaitForSeconds(0.3f); // ������ 0.3��
        isUpgradeOnCooldown = false;
    }

    private void ApplyStatByLevel()
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
}
