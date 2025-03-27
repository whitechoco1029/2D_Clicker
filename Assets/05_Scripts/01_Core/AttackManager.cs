using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance; // �̱��� ����

    public int clickDamage = 1; // Ŭ�� ���ݷ�
    public int autoDamage = 2;   // �ڵ� ���ݷ�
    public float autoAttackInterval = 1f; // �ڵ� ���� ����

    public Monster targetMonster;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        targetMonster = FindObjectOfType<Monster>(); // ���� ã��
        InvokeRepeating(nameof(AutoAttack), autoAttackInterval, autoAttackInterval); // �ڵ� ���� ����
    }

    public void ClickAttack()
    {
        if (targetMonster != null)
        {
            targetMonster.TakeDamage(clickDamage);

        }
    }

    public void AutoAttack()
    {
        if (targetMonster != null)
        {
            targetMonster.TakeDamage(autoDamage);
        }
    }
}