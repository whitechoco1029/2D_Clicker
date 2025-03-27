using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] EnemyData data;

    public float Health {  get; private set; }
    public float MaxHealth {  get; private set; }
    public int DropGold { get; private set; }

    void InitStatData(int stage)
    {
        // 스테이지에 따른 스탯 조정
        MaxHealth = data.health;
        Health = MaxHealth;
        DropGold = data.dropGold;
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0 )
        {
            Health = 0;

            Die();
        }
    }

    public virtual void Die()
    {

    }
}
