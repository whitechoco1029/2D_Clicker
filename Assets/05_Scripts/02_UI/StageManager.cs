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

    public EnemySpawner spawner;
    public UIStage uiStage;
    public int maxKillCount;

    [Header("Difficult Level")]
    [SerializeField] float difficultyMultiplier;
    public float Difficulty => difficultyMultiplier;

    public EnemyBase spawnEnemy { get; private set; }
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

    public void OnButtonAttackClick()
    {
        float atk = GameManager.Instance.userData.atk + (UIInventory.Instance.equipSlots.weapondata == null ? 0 : UIInventory.Instance.equipSlots.weapondata.weaponPower);
        float rand = Random.Range(0f, 100f);
        bool critical = false;
        // 크리티컬
        if (rand <= GameManager.Instance.userData.critHit)
        {
            critical = true;
            atk *= GameManager.Instance.userData.critDmg;
        }

        atk = atk + (atk * Random.Range(-0.1f, 0.1f));

        spawnEnemy.TakeDamage(atk, critical);
    }
}
