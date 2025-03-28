using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager Instance;  // �̱���
    public ClickAttack clickAttack;  // Ŭ�� ���� ��ũ��Ʈ ����
    public AutoAttack autoAttack;  // �ڵ� ���� ��ũ��Ʈ ����

    public EnemySpawner enemySpawner;  // �� ������ ����ϴ� EnemySpawner ����

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

    //// ���ο� �� ����
    //public void SpawnNewEnemy()
    //{
    //    if (enemySpawner != null)
    //    {
    //        GameObject newEnemyObj = enemySpawner.SpawnMonster();
    //        spawnMonster = newEnemyObj.GetComponent<EnemySpawner>();

    //        // AutoAttack ��ũ��Ʈ�� �� �� ����
    //        if (autoAttack != null)
    //        {
    //            autoAttack.SetTarget(spawnMonster);
    //        }
    //    }
    //}

    //// Ŭ�� ���� ����
    //public void ClickAttack()
    //{
    //    if (clickAttack != null)
    //    {
    //        clickAttack.(spawnMonster);
    //    }
    //}

    //// �ڵ� ���� ����
    //public void AutoAttack()
    //{
    //    if (autoAttack != null)
    //    {
    //        autoAttack.AutoAttackRoutine(spawnMonster);
    //    }
    //}

    // ���� �׾��� �� ȣ���
    public void OnEnemyDeath()
    {
        Debug.Log("�� óġ �Ϸ�! ���ο� �� ����");
        SpawnNewEnemy();
    }
}
