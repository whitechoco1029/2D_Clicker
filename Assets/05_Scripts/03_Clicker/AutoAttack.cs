using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    private float attackInterval = 1f; // 1초 간격
    private float AutoDmg = 2f; // 공격력

    void Start()
    {
        StartCoroutine(AutoAttackCoroutine());
    }
    
    IEnumerator AutoAttackCoroutine()
    {
        while (true)
        {
            attackInterval = 1 / GameManager.Instance.userData.atkSpeed;
            yield return new WaitForSeconds(attackInterval);
            StageManager.Instance.OnButtonAttackClick();
            //AttackAllEnemies();
        }
    }

    void AttackAllEnemies()
    {
        Debug.Log("자동 공격!");
        foreach (EnemyBase enemy in FindObjectsOfType<EnemyBase>()) // 모든 적 찾기
        {
            enemy.TakeDamage(AutoDmg);
        }
    }
}
