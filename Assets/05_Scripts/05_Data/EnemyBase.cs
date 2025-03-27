using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] EnemyData data;

    public float Health {  get; private set; }
    public int DropGold { get; private set; }

    void InitStatData(int stage)
    {
        Health = data.health;
        DropGold = data.dropGold;
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0 )
        {
            Die();
        }
    }

    public virtual void Die()
    {

    }
}
