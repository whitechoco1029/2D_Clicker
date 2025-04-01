using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "UpgradeStatData", menuName = "GameData/UpgradeStatData", order = 1)]
public class UpgradeStatData : ScriptableObject
{
    public StatType statType;
    public List<float> valuePerLevel = new List<float>();  // ������ ���� ��ġ
    public List<float> costPerLevel = new List<float>();   // ������ ���׷��̵� ���

    public float GetValueByLevel(int level)
    {
        if (level < valuePerLevel.Count)
            return valuePerLevel[level];
        return valuePerLevel[^1]; // ������ �� ��ȯ
    }

    public float GetCostByLevel(int level)
    {
        if (level < costPerLevel.Count)
            return costPerLevel[level];
        return costPerLevel[^1]; // ������ �� ��ȯ
    }

#if UNITY_EDITOR
    [ContextMenu("�ڵ� ����: ������ ���")]
    public void GenerateExponentialData()
    {
        valuePerLevel.Clear();
        costPerLevel.Clear();

        float baseValue = 1f;
        float baseCost = 1000f;
        float growthRate = 1.1f;       // ���� �����
        float costGrowthRate = 1.2f;   // ��� �����

        for (int level = 1; level <= 1000; level++)
        {
            float value = baseValue * Mathf.Pow(growthRate, level);
            float cost = baseCost * Mathf.Pow(costGrowthRate, level);

            // ġ��Ÿ Ȯ���� �ִ� 100���� ����
            if (statType == StatType.CritHit)
                value = Mathf.Min(value, 100f);

            valuePerLevel.Add(Mathf.Round(value * 100f) / 100f); // �Ҽ��� 2�ڸ�
            costPerLevel.Add(Mathf.Round(cost));

            valuePerLevel.Add(Mathf.Round(value * 1000f) / 1000f); // �Ҽ��� 2�ڸ� �ݿø�
            costPerLevel.Add(Mathf.Round(cost));
        }

        Debug.Log($"[{statType}] ������ ��� �ڵ� ���� �Ϸ�!");
        EditorUtility.SetDirty(this); // ���� ����
    }
#endif
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
