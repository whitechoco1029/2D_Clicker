using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    ObjectPool<GameObject>[] enemyPool;

    private void Start()
    {
        enemyPool = new ObjectPool<GameObject>[enemyPrefabs.Length];

        for (int i = 0; i < enemyPool.Length; i++)
        {
            enemyPool[i] = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(enemyPrefabs[i]),
                actionOnGet: (enemy) => enemy.SetActive(true),
                actionOnRelease: (enemy) => enemy.SetActive(false),
                actionOnDestroy: (enemy) => Destroy(enemy)
                );
        }
    }

    public GameObject SpawnMonster()
    {
        int rand = Random.Range(0, enemyPool.Length);

        return enemyPool[rand].Get();
    }
}
