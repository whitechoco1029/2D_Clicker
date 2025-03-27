using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance; // 싱글톤 패턴

    public int clickDamage = 1; // 클릭 공격력
    public int autoDamage = 2;   // 자동 공격력
    public float autoAttackInterval = 1f; // 자동 공격 간격

    public Monster targetMonster;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        targetMonster = FindObjectOfType<Monster>(); // 몬스터 찾기
        InvokeRepeating(nameof(AutoAttack), autoAttackInterval, autoAttackInterval); // 자동 공격 실행
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