using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

public class StageManager : MonoBehaviour
{
    static StageManager instance;
    public static StageManager Instance => instance;

    [SerializeField] EnemySpawner spawner;
    public UIStage uiStage;
    public int maxKillCount;
    [SerializeField] ParticleSystem particle;

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
        particle.Stop();

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

    public void TestButtonEvent()
    {
        spawnEnemy.TakeDamage(10);
    }

    public void CreateHitParticles(Vector3 position, Color particleColor)
    {
        // ��ƼŬ ������ ������ġ ����
        Vector3 vecRand = Random.insideUnitCircle * 0.5f;
        particle.transform.position = position + vecRand;

        // ��ƼŬ �÷�
        ParticleSystem.MainModule main = particle.main;
        main.startColor = particleColor;

        particle.Play();
    }
}
