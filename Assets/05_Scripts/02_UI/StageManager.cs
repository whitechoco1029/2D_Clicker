using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class StageManager : MonoBehaviour
{
    static StageManager instance;
    public static StageManager Instance => instance;

    [SerializeField] EnemySpawner spawner;
    public UIStage uiStage;
    public int maxKillCount;

    EnemyBase spawnEnemy;
    int stage;
    int killCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        stage = 1;
        killCount = 0;

        SpawnEnemy();
    }
    
    void SpawnEnemy()
    {
        spawnEnemy = spawner.SpawnEnemy();
        spawnEnemy.InitStatData(stage);

        uiStage.UpdateHealthBar(spawnEnemy.MaxHealth, spawnEnemy.Health);
    }

    public void AddKillCount()
    {
        spawner.ReleaseEnemy(spawnEnemy);

        killCount++;

        if (killCount > maxKillCount)
        {
            killCount = 0;
            NextStage();
        }

        uiStage.UpdateKillCount(killCount);

        SpawnEnemy();
    }

    public void NextStage()
    {
        stage++;

        uiStage.UpdateStageUI(stage);
    }
}
