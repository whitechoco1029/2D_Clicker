using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] EnemyData data;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Rigidbody2D rigid;

    
    [Header("Effect")]
    [SerializeField] float flashDuration;
    [SerializeField] float fadeDuration;

    public float Health {  get; private set; }
    public float MaxHealth {  get; private set; }
    public int DropGold { get; private set; }

    Coroutine coroutine;
    bool dead;
    
    public void Init(int stage)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        sprite.color = Color.white;
        dead = false;

        // 스테이지에 따른 스탯 조정
        MaxHealth = data.health * Mathf.Pow(StageManager.Instance.Difficulty, stage - 1);
        Health = MaxHealth;
        DropGold = data.dropGold;
    }

    public virtual void TakeDamage(float damage)
    {
        if (dead) return;

        Health -= damage;
        Health = Mathf.Max(0, Health);
        //tageManager.Instance.uiStage.UpdateHealthBar(MaxHealth, Health);
        //ParticleManager.Instance.CreateDamageParticle(transform.position, damage.ToString(), Color.yellow);

        if (Health <= 0)
            Die();
        else
        {
            if (coroutine != null)
            {
                StopAllCoroutines();
            }

            coroutine = StartCoroutine(FlashOnHit());
        }
    }

    public virtual void Die()
    {
        dead = true;

        StopAllCoroutines();
        sprite.color = Color.white;

        // 날아가는 효과
        Vector2 force = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
        rigid.AddForce(force * 10f, ForceMode2D.Impulse);
        rigid.AddTorque(Random.Range(-100f, 100f), ForceMode2D.Impulse);

        StartCoroutine(FadeOut());
    }
    
    IEnumerator FlashOnHit()
    {
        sprite.color = Color.red;
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            sprite.color = Color.Lerp(Color.red, Color.white, elapsed / flashDuration);

            yield return null;
        }

        sprite.color = Color.white;
    }

    IEnumerator FadeOut()
    {
        Color color = sprite.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            color.a = Mathf.Lerp(1f, 0, elapsed / fadeDuration);
            sprite.color = color;
            yield return null;
        }

        StageManager.Instance.AddKillCount();
    }
}
