using UnityEngine;
using System.Collections;

public class MonsterAttack : MonoBehaviour
{
    public float attackInterval = 2.0f; // 몬스터 공격 간격
    public int attackDamage = 10; // 몬스터의 공격력
    private GameObject player; // 플레이어 대상

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(AutoAttackRoutine());
    }

    IEnumerator AutoAttackRoutine()
    {
        while (player != null)
        {
            yield return new WaitForSeconds(attackInterval);

            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        Debug.Log("몬스터가 플레이어를 공격! 데미지: " + attackDamage);
        // 여기에 플레이어 체력 감소 로직 추가 가능
    }
}