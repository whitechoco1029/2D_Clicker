using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] EnemyData data;
    [SerializeField] SpriteRenderer sprite;

    [Header("HitEffect")]
    [SerializeField] float flashDuration;

    public float Health {  get; private set; }
    public float MaxHealth {  get; private set; }
    public int DropGold { get; private set; }

    public void InitStatData(int stage)
    {
        // 스테이지에 따른 스탯 조정
        MaxHealth = data.health;
        Health = MaxHealth;
        DropGold = data.dropGold;
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        Health = Mathf.Max(0, Health);
        StageManager.Instance.uiStage.UpdateHealthBar(MaxHealth, Health);

        StartCoroutine(FlashOnHit());

        if (Health <= 0)
            Die();
    }

    public virtual void Die()
    {
        StageManager.Instance.AddKillCount();
    }
    
    IEnumerator FlashOnHit()
    {
        sprite.color = Color.red;
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            sprite.color = Color.Lerp(sprite.color, Color.white, elapsed / flashDuration);

            yield return null;
        }

        sprite.color = Color.white;
    }
}
