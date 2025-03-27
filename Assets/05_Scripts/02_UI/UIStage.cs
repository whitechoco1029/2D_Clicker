using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textStage;
    [SerializeField] TextMeshProUGUI textKillCount;
    [SerializeField] Image healthBar;

    public void UpdateStageUI(int stage)
    {
        textStage.text = $"Stage {stage}";
    }

    public void UpdateKillCount(int cnt)
    {
        textStage.text = $"{cnt} / 10";
    }

    public void UpdateHealthBar(float maxHealth, float curHealth)
    {
        healthBar.fillAmount = curHealth / maxHealth;
    }
}
