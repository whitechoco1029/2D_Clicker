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

        EnemyBase newEnemy;
        for (int i = 0;  i < enemyPrefabs.Length; i++)
        {
            newEnemy = Instantiate(enemyPrefabs[i], transform);

            enemyPool.Add(newEnemy);
            newEnemy.gameObject.SetActive(false);
        }
    }

    public EnemyBase SpawnEnemy()
    {
        int rand = Random.Range(0, enemyPool.Count);
        enemyPool[rand].gameObject.SetActive(true);

        return enemyPool[rand];
    }

    public void ReleaseEnemy(EnemyBase enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
