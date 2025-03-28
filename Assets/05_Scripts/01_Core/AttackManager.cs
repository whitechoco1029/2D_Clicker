using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance;  // 싱글톤
    public ClickAttack clickAttack;  // 클릭 공격 스크립트 참조
    public AutoAttack autoAttack;  // 자동 공격 스크립트 참조

    public EnemySpawner enemySpawner;  // 적 스폰을 담당하는 EnemySpawner 참조

    public void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        clickAttack = GetComponent<ClickAttack>();
        autoAttack = GetComponent<AutoAttack>();

        SpawnNewEnemy();
    }

    //// 새로운 적 스폰
    //public void SpawnNewEnemy()
    //{
    //    if (enemySpawner != null)
    //    {
    //        GameObject newEnemyObj = enemySpawner.SpawnMonster();
    //        spawnMonster = newEnemyObj.GetComponent<EnemySpawner>();

    //        // AutoAttack 스크립트에 새 적 전달
    //        if (autoAttack != null)
    //        {
    //            autoAttack.SetTarget(spawnMonster);
    //        }
    //    }
    //}

    //// 클릭 공격 실행
    //public void ClickAttack()
    //{
    //    if (clickAttack != null)
    //    {
    //        clickAttack.(spawnMonster);
    //    }
    //}

    //// 자동 공격 실행
    //public void AutoAttack()
    //{
    //    if (autoAttack != null)
    //    {
    //        autoAttack.AutoAttackRoutine(spawnMonster);
    //    }
    //}

    // 적이 죽었을 때 호출됨
    public void OnEnemyDeath()
    {
        Debug.Log("적 처치 완료! 새로운 적 스폰");
        SpawnNewEnemy();
    }
}
