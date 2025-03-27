using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class StageManager : MonoBehaviour
{
    static StageManager instance;
    public static StageManager Instance => instance;

    public UIStage uiStage;
    public int maxKillCount;

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
    }

    public void AddKillCount()
    {
        killCount++;

        if (killCount > maxKillCount)
        {
            killCount = 0;
            NextStage();
        }

        uiStage.UpdateKillCount(killCount);
    }

    public void NextStage()
    {
        stage++;

        uiStage.UpdateStageUI(stage);
    }
}
