using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textStage;
    [SerializeField] TextMeshProUGUI textKillCount;

    [Header ("HPBar Info")]
    [SerializeField] Image healthBar;
    [SerializeField] float decreaseDuration;

    Coroutine coroutine;
    float targetHp;
    float curHp;

    private void Awake()
    {
        StageManager.Instance.uiStage = this;
    }

    public void UpdateStageUI(int stage)
    {
        textStage.text = $"Stage {stage}";
    }

    public void UpdateKillCount(int cnt)
    {
        textKillCount.text = $"{cnt} / {StageManager.Instance.maxKillCount}";
    }

    public void InitHpBar(float maxHealth)
    {
        if (coroutine != null)
        {
            StopAllCoroutines();
        }

        curHp = maxHealth;
        healthBar.fillAmount = curHp / maxHealth;
    }

    public void UpdateHealthBar(float maxHealth, float curHealth)
    {
        if (coroutine != null)
        {
            StopAllCoroutines();
        }
        
        coroutine = StartCoroutine(ChangeHealthBar(maxHealth, curHealth));
    }

    IEnumerator ChangeHealthBar(float maxHealth, float curHealth)
    {
        targetHp = curHealth;
        float elapsed = 0f;

        while (elapsed < decreaseDuration)
        {
            elapsed += Time.deltaTime;
            curHp = Mathf.Lerp(curHp, targetHp, elapsed / decreaseDuration);
            healthBar.fillAmount = curHp / maxHealth;

            yield return null;
        }

        curHp = targetHp;
        healthBar.fillAmount = curHp / maxHealth;
    }
}
