using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public EnemyBase[] enemyPrefabs;

    List<EnemyBase> enemyPool;

    private void Start()
    {
        enemyPool = new List<EnemyBase>();

        for (int i = 0;  i < enemyPrefabs.Length; i++)
        {
            enemyPool.Add(Instantiate(enemyPrefabs[i], transform));
        }
    }

    public EnemyBase SpawnEnemy()
    {
        int rand = Random.Range(0, enemyPool.Count);

        return enemyPool[rand];
    }

    public void ReleaseEnemy(EnemyBase enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
