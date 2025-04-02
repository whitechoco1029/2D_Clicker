using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    private float attackInterval = 1f; // 1�� ����
    private float AutoDmg = 2f; // ���ݷ�

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
        Debug.Log("�ڵ� ����!");
        foreach (EnemyBase enemy in FindObjectsOfType<EnemyBase>()) // ��� �� ã��
        {
            enemy.TakeDamage(AutoDmg);
        }
    }
}
