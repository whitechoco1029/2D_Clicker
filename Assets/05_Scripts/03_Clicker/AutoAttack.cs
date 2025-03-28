using System.Collections;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public  void Start()
    {
        StartCoroutine(AutoAttackRoutine());  // 코루틴 시작
    }

    public IEnumerator AutoAttackRoutine()
    {
        while (true)  // 무한 루프 (조건에 따라 공격 실행)
        {
            if (AttackManager.Instance != null)
            {
                AttackManager.Instance.AutoAttack();  // 공격 실행
            }

            yield return new WaitForSeconds(1f);  // 1초마다 실행
        }
    }
}