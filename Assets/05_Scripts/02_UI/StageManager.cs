using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class StageManager : MonoBehaviour
{
    static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameObject("StageManager").AddComponent<StageManager>();

            return instance;
        }
    }

    public EnemySpawner spawner;
    public UIStage uiStage;
    public int maxKillCount;

    [Header("Difficult Level")]
    [SerializeField] float difficultyMultiplier;
    public float Difficulty => difficultyMultiplier;

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
        spawnEnemy.Init(stage);

        uiStage.InitHpBar(spawnEnemy.MaxHealth);
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

    public void TestButtonEvent()
    {
        spawnEnemy.TakeDamage(10);
    }
}
