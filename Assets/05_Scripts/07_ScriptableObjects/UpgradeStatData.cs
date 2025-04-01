using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "UpgradeStatData", menuName = "GameData/UpgradeStatData", order = 1)]
public class UpgradeStatData : ScriptableObject
{
    public StatType statType;
    public List<float> valuePerLevel = new List<float>();  // 레벨별 증가 수치
    public List<float> costPerLevel = new List<float>();   // 레벨별 업그레이드 비용

    public float GetValueByLevel(int level)
    {
        if (level < valuePerLevel.Count)
            return valuePerLevel[level];
        return valuePerLevel[^1]; // 마지막 값 반환
    }

    public float GetCostByLevel(int level)
    {
        if (level < costPerLevel.Count)
            return costPerLevel[level];
        return costPerLevel[^1]; // 마지막 값 반환
    }

#if UNITY_EDITOR
    [ContextMenu("자동 생성: 곱연산 기반")]
    public void GenerateExponentialData()
    {
        valuePerLevel.Clear();
        costPerLevel.Clear();

        float baseValue = 1f;
        float baseCost = 1000f;
        float growthRate = 1.1f;       // 스탯 성장률
        float costGrowthRate = 1.2f;   // 비용 성장률

        for (int level = 1; level <= 1000; level++)
        {
            float value = baseValue * Mathf.Pow(growthRate, level);
            float cost = baseCost * Mathf.Pow(costGrowthRate, level);

            // 치명타 확률은 최대 100으로 제한
            if (statType == StatType.CritHit)
                value = Mathf.Min(value, 100f);

            valuePerLevel.Add(Mathf.Round(value * 100f) / 100f); // 소수점 2자리
            costPerLevel.Add(Mathf.Round(cost));

            valuePerLevel.Add(Mathf.Round(value * 1000f) / 1000f); // 소수점 2자리 반올림
            costPerLevel.Add(Mathf.Round(cost));
        }

        Debug.Log($"[{statType}] 곱연산 기반 자동 생성 완료!");
        EditorUtility.SetDirty(this); // 변경 저장
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
